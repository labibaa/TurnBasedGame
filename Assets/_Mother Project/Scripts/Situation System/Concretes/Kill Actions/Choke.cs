using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Choke : ICommand
{
    public UniTask Execute()
    {
        throw new System.NotImplementedException();
    }

    public string GetActionName()
    {
        throw new System.NotImplementedException();
    }

    public string GetActionType()
    {
        throw new System.NotImplementedException();
    }

    public NavMeshAgent GetAgent()
    {
        throw new System.NotImplementedException();
    }

    public int GetAPValue()
    {
        throw new System.NotImplementedException();
    }

    public List<GameObject> GetPaths()
    {
        throw new System.NotImplementedException();
    }

    public int GetPVValue()
    {
        throw new System.NotImplementedException();
    }

    public CharacterBaseClasses GetTarget()
    {
        throw new System.NotImplementedException();
    }
}
