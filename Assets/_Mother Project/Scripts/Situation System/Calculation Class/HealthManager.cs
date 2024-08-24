using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;

public class HealthManager : MonoBehaviour
{
    // public static HealthCalculation Instance { get; private set; }


    public static event Action OnGridDisable;

    public static HealthManager instance;

    PlayerTurn deadPlayerTurn;

    [SerializeField] PlayerTurn playerTurn;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
       
    }

    public int HealthCalculation(float damage,int health)
    {
        //do some health stuff
        health = health-(int)damage;
        if (health<0)
        {
            return 0;
        }

        return health;
    }
    public int ResolveCalculation(float damage, int resolve)
    {
        //do some health stuff
        resolve = resolve - (int)damage;
        if (resolve < 0)
        {
            return 0;
        }
        return resolve;
    }



    public async UniTask PlayerMortality(TemporaryStats  playerStat,int attackOrder)
    {
        
        if (playerStat.CurrentHealth < 1)
        {
            deadPlayerTurn = playerStat.gameObject.GetComponent<PlayerTurn>();
            playerStat.playerMortality = Mortality.Dead;
            Debug.Log(deadPlayerTurn.name + "dead");
            TurnManager.instance.players.Remove(deadPlayerTurn);
            Debug.Log(deadPlayerTurn.name + "dead20");
            TurnManager.instance.target.Remove(deadPlayerTurn);
            Debug.Log(deadPlayerTurn.name + "dead329");

            if (!RemoveAdjacentDuplicates(TurnManager.instance.players) && CheckIfElementIsDuplicate(TurnManager.instance.players,playerTurn ))
            {
                TurnManager.instance.target.Remove(TurnManager.instance.players[TurnManager.instance.players.Count-1]);
                TurnManager.instance.players.Remove(TurnManager.instance.players[TurnManager.instance.players.Count-1]);
            }
            //Destroy(gameObject);
            Vector2 deadPlayerGridPosition=GridSystem.instance.WorldToGrid(playerStat.transform.position);
            GridSystem.instance._gridArray[(int)deadPlayerGridPosition.x, (int)deadPlayerGridPosition.y].GetComponent<GridStat>().ClearGrid();
            TeamManager.instance.RemovePlayerFromTeamList(playerStat);
            TeamManager.instance.PrintDictionary();
            playerStat.gameObject.SetActive(false);
            if (attackOrder<0)
            {
                TurnManager.instance.currentPlayerIndex--;
            }
           
            
            Debug.Log("Current" +
            TurnManager.instance.currentPlayerIndex);
            if ( TeamManager.instance.IsAnyTeamEmpty())//TurnManager.instance.players.Count<2)
            {
                //UI.instance.SendNotification($"{TurnManager.instance.players[0].GetComponent<TemporaryStats>().CharacterTeam} has Won");
               
                //TurnManager.instance.players[0].gameObject.SetActive(false);
               
                PlayerStatUI.instance.CharacterUIList.Clear();
                TempManager.instance.currentState = GameStates.SituationOff;
                HandleTurnNew.instance.SituationEndCondition = true;
                TurnManager.instance.currentPlayerIndex = 0;
                //TeamManager.instance.ResetPlayerTeamList();
                //TeamManager.instance.PopulateTeamPlayerList();
                OnGridDisable?.Invoke();
                //UI.instance.inGameCanvas.SetActive(false);
                //UI.instance.winMenu.SetActive(true);

                if(TurnManager.instance.players[0].GetComponent<TemporaryStats>().CharacterTeam != TeamName.TeamA)
                {
                    UI.instance.inGameCanvas.SetActive(false);
                    UI.instance.winMenu.SetActive(true);
                }

            }
            else {
                UI.instance.SendNotification($"{playerStat.gameObject.name} has died");
                
            }
          
            foreach (PlayableCharacterUI playableCharacterUI in PlayerStatUI.instance.CharacterUIList)
            {
                if (playerStat.GetComponent<CharacterBaseClasses>() == playableCharacterUI.myCharacter)
                {
                    PlayerStatUI.instance.CharacterUIList.Remove(playableCharacterUI);
                    Destroy(playableCharacterUI.gameObject);
                   
                    break;
                   
                }
            }
           
            //HandleTurnNew.instance.allTurnsOfPlayer.RemoveAll(x => x.target == playerStat.GetComponent<CharacterBaseClasses>());
           
            //HandleTurnNew.instance.ProceedToNextTurn();
        }

    }

    public static bool RemoveAdjacentDuplicates(List<PlayerTurn> list)
    {
        if (list.Count < 2) // Handle lists with less than 2 elements
        {
            return false;
        }

        for (int i = 0; i < list.Count - 1; i++) // Iterate up to the second-last element
        {
            if (list[i] == list[i + 1])
            {
                TurnManager.instance.target.Remove(list[i+1]);
                list.RemoveAt(i + 1);
                return true;// Remove the later duplicate
            }
        }
        return false;
    }

    public bool CheckIfElementIsDuplicate<T>(List<T> list, T element)
    {
        int count = 0;
        foreach (T item in list)
        {
            if (EqualityComparer<T>.Default.Equals(item, element))
            {
                count++;
                if (count > 1)
                {
                    return true;
                }
            }
        }
        return false;
    }


}


