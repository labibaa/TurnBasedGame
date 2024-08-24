using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArray : MonoBehaviour
{
    public GameObject[] playerGameObjects; // Array of player GameObjects
    int i=0;

    private void OnEnable()
    {
        TempManager.GameStateChanged += OnGameStateChange;
    }
    private void OnDisable()
    {
        TempManager.GameStateChanged -= OnGameStateChange;
    }

    private void OnGameStateChange(GameStates states)
    {
        //if(states == GameStates.PlayerSelection)
        //{
        //    Iterate();
        //}
        //if(states == GameStates.FinishTurn)
        //{
        //    Iterate();
        //}
        
    }

    public GameObject Iterate()
    {
        i++;
        if (i < playerGameObjects.Length)
        {
            Debug.Log(playerGameObjects[i]);
            TempManager.instance.OnGameState(GameStates.StartTurn);
            return playerGameObjects[i];

        }
        else
        {
            TempManager.instance.OnGameState(GameStates.EnemyTurn);
        }
       

        return null;
    }

  


}