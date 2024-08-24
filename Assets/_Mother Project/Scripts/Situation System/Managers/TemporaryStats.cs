using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TemporaryStats : MonoBehaviour
{
    public int CurrentHealth;
    public int PlayerHealth;
    public GameObject Pathmark;
    public int CurrentAP;
    public int PlayerAP;
    public int CurrentResolve;
    public bool IsBlockActive;
    public bool IsDodgeActive; 
    public bool IsThirdRatePerformanceActive;
    public bool IsCounterActive;
    public float CurrentEndurance;
    public int CurrentDex;
    public bool AutoMove;
    public int playerUltimateBarCount=0;
    public GameObject PlayerUltimateBar; 
    
    public Mortality playerMortality;
    
    public Vector3 currentPlayerGridPosition;
    [SerializeField]
    Vector3 _spawnPosition;
    [SerializeField]
    Vector2 gridCoordinateSpawn;

    public GameObject lastPosition;
  
    public Image playerStatPanel;
    public GameObject PlayerActionListPanel;
    CharacterBaseClasses _characterBaseClasses;

    public Transform FlyingTextParent;
    CinemachineVirtualCamera playerCam;

    public TeamName CharacterTeam;
    public GameObject SelectionParticle;
    public GameObject EnemyTargetSelectionParticle;
    public int playerVisiblity=1;

    //Animator animator;

    private void OnEnable()
    {
       

        //GridSystem.OnGridGenerationSpawn += AssignSpawnPosition;
        HealthManager.OnGridDisable += onEndFunction;

    }

    private void OnDisable()
    {
        
        //GridSystem.OnGridGenerationSpawn -= AssignSpawnPosition;
        HealthManager.OnGridDisable -= onEndFunction;
    }


    private void Start()
    {
        onEndFunction();
        
        //s
        // Call the function MyFunction after one second
        //Invoke("AssignPosition", 1.0f);
    }


    void onEndFunction()
    {
       // currentPlayerGridPosition = transform.position;
       
        

        PlayerActionListPanel = ButtonStackManager.instance.PopulateActionPanel(_characterBaseClasses);
        PlayerUltimateBar = ButtonStackManager.instance.PopulateUltimateBar(_characterBaseClasses);
        PlayerActionListPanel.SetActive(false);
        PlayerUltimateBar.SetActive(false);
    }


    private void Awake()
    {
        _characterBaseClasses = GetComponent<CharacterBaseClasses>();
       // animator = GetComponent<Animator>();
    }

    public TemporaryStats(int health,int ap,int Dex)
    {
        CurrentHealth = health;
        CurrentAP = ap;
        CurrentDex = Dex;
    }
   

    public void AssignSpawnPosition()
    {



       // animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash);

        if ( gridCoordinateSpawn.x< GridSystem.instance._gridArray.GetLength(0) && gridCoordinateSpawn.x < GridSystem.instance._gridArray.GetLength(1))
        {
           

            transform.position = GridSystem.instance._gridArray[(int)gridCoordinateSpawn.x, (int)gridCoordinateSpawn.y].transform.position;
        }

        
        currentPlayerGridPosition = transform.position;
        transform.position = currentPlayerGridPosition;


        Debug.Log("hh: "+ currentPlayerGridPosition + "hh222" + transform.position);
        playerMortality = Mortality.Alive;
        OrbSpawner.instance.PlayerReadyCounter();
        ResetStatForGrid();
        TeamManager.instance.AddPlayerToTeamList(this);


        GridPlayerAnimation gridPlayerAnimation;
        if (TryGetComponent<GridPlayerAnimation>(out gridPlayerAnimation))
        {
            // Call the SetMoveAnimation method
            
            gridPlayerAnimation.SetMoveAnimation(0, 1);
        }

    }

    private void ResetStatForGrid()
    {
        IsBlockActive = false;
        IsDodgeActive = false;
        AutoMove = false;
        playerVisiblity = 1;
        playerUltimateBarCount = 0;
        playerMortality = Mortality.Alive;
        GetComponent<PlayerTurn>().isMoveOn = true;
        CurrentAP = PlayerAP;
        CurrentHealth = PlayerHealth;

    }

    public void SelectTarget()
    {
        if (GridSystem.instance.IsGridOn)
        {

            if(TempManager.instance.currentState == GameStates.TargetSelectionTurn && TurnManager.instance.targetsInRange.Contains(_characterBaseClasses))
            {
                TempManager.instance.defender = gameObject;
                //ActionArchive.instance.GetPlayerStats();
                TempManager.instance.ChangeGameState(GameStates.MidTurn);
                DictionaryManager.instance.GiveAction(TempManager.instance.actionName);

                //DictionaryManager.instance.GiveAction(TempManager.instance.actionName).Invoke();
                GridMovement.instance.ResetHighlightedPath();
                TurnManager.instance.ResetTargetHIghlightVisual();
                TurnManager.instance.targetsInRange.Clear();
                TurnManager.instance.nonCharacterTargetsInRange.Clear();
            }

            //UI.instance.ShowPanel(playerStatPanel);
            //playerStatPanel.gameObject.SetActive(true);
            //PlayerStatUI.instance.GetPlayerStatDetails(GetComponent<CharacterBaseClasses>());
        }
    }
}
