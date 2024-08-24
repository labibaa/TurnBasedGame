using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerTurn : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    public bool isMoveOn =true;
    public bool myTurn;

    private void Awake()
    {
        isMoveOn = true;
        //turnManager = FindObjectOfType<TurnManager>();
    }

    //cinemachine
    public void StartTurn()
    {
        // Enable player controls/actions for their turn
        // Implement your specific turn-based logic here
    }

    public void EndTurn()
    {
        // Disable player controls/actions
        // Implement any necessary cleanup or transition to the next turn
        turnManager.EndTurn();

    }
}
