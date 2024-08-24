using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NushratGameManager : MonoBehaviour
{
    /*public GameStates States;
    public PlayerType PlayerType;

    PlayerArray PlayerArrayList;

    public int ApCost = 6;

    public static NushratGameManager instance;

    //public static event Action<GameStates> GameStateChanged;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void OnGameState(GameStates newStates)
    {
        States = newStates;

        switch(newStates)
        {
            case GameStates.PlayerSelection:
                SelectPlayer();
                break;
            case GameStates.StartTurn:
                TurnStarted();
                break;
            case GameStates.MidTurn:
                TurnMid();
                break;
            case GameStates.FinishTurn:
                TurnFinished();
                break;
            case GameStates.EnemyTurn:
                StartEnemyTurn();
                break;
            case GameStates.Simulation:
                StartSimulation();
                break;

        }


        //GameStateChanged?.Invoke(newStates); 


    }

    private void StartSimulation()
    {
        Debug.Log("simulation");
    }

    private void StartEnemyTurn()
    {
        OnGameState(GameStates.Simulation);
    }

    private void TurnFinished()
    {
       OnGameState(GameStates.PlayerSelection);
    }

    private void TurnMid()
    {
        if(ApCost >= 6)
        {
            OnGameState(GameStates.FinishTurn);
        }
        
    }

    private void TurnStarted()
    {
       
    }

    private void SelectPlayer()
    {
       // PlayerArrayList.Iterate();
    }
*/
}
