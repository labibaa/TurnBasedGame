using StarterAssets;
using System.Collections.Generic;
using UnityEngine;

public class GridActivation : MonoBehaviour
{

    public static GridActivation instance;
    [SerializeField]
    public List<GameObject> players = new List<GameObject>();
    [SerializeField]
    List<GameObject> uiGameObjects = new List<GameObject>();
    [SerializeField]
    List<GameObject> gameObjectsTobeDisabled = new List<GameObject>();
    [SerializeField]
    List<GameObject> gameObjectsTobeEnabled = new List<GameObject>();

    [SerializeField]
    public GameObject playableCharacter;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    GameObject grid;
    [SerializeField]
    GameObject miniStatUI;
    [SerializeField]
    GameObject cameraController;


    [SerializeField]
    GameObject gridAudio;






    int count =0;
    private void OnEnable()
    {
        WaveManager.OnGridReady += EnableSituaionUI;
        HealthManager.OnGridDisable += DisableSituationSystem;
       // GridSystem.OnGridGeneration += HandleCharacterSpawn;
        HealthManager.OnGridDisable += DisableSituaionUI;
        HealthManager.OnGridDisable += HandleCharacterDeSpawn ;
       // cameraController.SetActive(true);
    }
    private void OnDisable()
    {
        WaveManager.OnGridReady -= EnableSituaionUI;
        HealthManager.OnGridDisable -= DisableSituationSystem;
        //GridSystem.OnGridGeneration -= HandleCharacterSpawn;
        HealthManager.OnGridDisable -= DisableSituaionUI;
        HealthManager.OnGridDisable -= HandleCharacterDeSpawn;
        //cameraController.SetActive(false);
    }
//Failsafe2

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
      //GridSystem.instance.GenerateGridOnButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GridSystem.instance.IsGridOn && count < 2)
        {
            
            GridSystem.instance.GenerateGridOnButton();
        }
    }


    private void Awake()
    {
        instance = this;
    }

    private async void EnableSituaionUI()
    {
        //await UI.instance.AnimatePanelAsync();
        foreach (GameObject ui in uiGameObjects)
        {
            ui.SetActive(true);
        }
        PlayerStatUI.instance.CreateSummaryList();
        DisableUIObjects();
        TurnManager.instance.StartTurn();
        gridAudio.GetComponent<AudioSource>().enabled = true;
        gridAudio.GetComponent<AudioSource>().Play();
        this.GetComponent<BoxCollider>().enabled = false;

    }


    private async void DisableSituaionUI()
    {
        //await UI.instance.AnimatePanelAsync();
        foreach (GameObject ui in uiGameObjects)
        {
            ui.SetActive(false);
        }
       
        
        //PlayerStatUI.instance.CreateSummaryList();
        EnableUIObjects();
        gridAudio.GetComponent<AudioSource>().Stop();
        gridAudio.GetComponent<AudioSource>().enabled = false;
       

    }




    private void DisableSituationSystem()
    {
        playableCharacter.SetActive(true);

        foreach (Transform child in grid.transform)
        {
            // Destroy the child GameObject
            Destroy(child.gameObject);
        }

        //Transform[] children = miniStatUI.GetComponentsInChildren<Transform>();



        foreach (Transform child in miniStatUI.transform)
        {
            Destroy(child.gameObject);
        }




        //playableCharacter.GetComponent<CharacterController>().enabled = true;
        playableCharacter.GetComponent<ThirdPersonController>().enabled = true;
        playableCharacter.GetComponent<ThirdPersonController>().DisableAnim();
        
        //player.GetComponent<PlayerMove>().enabled = true;



        //might need to refactor this part, putting them on  a funciton
        //player.GetComponent<GridInput>().enabled = true;
        playableCharacter.GetComponent<GridPlayerAnimation>().enabled = false;
        gameManager.GetComponent<GridMovement>().enabled = false;
        gameManager.GetComponent<TurnManager>().enabled = false;


        TempManager.instance.SituationUIPanel.SetActive(false);
        TempManager.instance.UlimateUIPanel.SetActive(false);

        GridSystem.instance.IsGridOn = false;
       // HandleTurnNew.instance.SituationEndCondition = false;

    }
    public void HandleCharacterSpawn()
    {
        foreach (GameObject p in players)
        {
            p.SetActive(true);
        }

        foreach (GameObject p in gameObjectsTobeEnabled)
        {
            p.SetActive(true);
        }
    }

    private void HandleCharacterDeSpawn()
    {
        foreach (GameObject p in players)
        {
            p.SetActive(false);
        }

        foreach (GameObject p in gameObjectsTobeEnabled)
        {
            p.SetActive(false);
        }
    }


    private void DisableUIObjects()
    {
        foreach (GameObject gameObjectsUI in gameObjectsTobeDisabled)
        {
            gameObjectsUI.SetActive(false);
        }
        
    }

    private void EnableUIObjects()
    {
        foreach (GameObject gameObjectsUI in gameObjectsTobeDisabled)
        {
            gameObjectsUI.SetActive(true);
        }
    }





}
