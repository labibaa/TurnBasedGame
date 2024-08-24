using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Turn
{
    public CharacterBaseClasses Player;
    public ICommand Command;
    public int PriorityValue;
    public CharacterBaseClasses target;
    public bool IsPerformed;



    public Turn(CharacterBaseClasses player,ICommand command,int priorityValue)
    {
        Player = player;
        Command = command;
        
        PriorityValue = priorityValue;
        target = command.GetTarget();

    }
}
