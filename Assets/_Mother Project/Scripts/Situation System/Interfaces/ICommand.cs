using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface ICommand 
{

    

   UniTask Execute();
   int GetPVValue();
   int GetAPValue();
   string GetActionName();
   CharacterBaseClasses GetTarget();
   NavMeshAgent GetAgent();

   List<GameObject> GetPaths();

   string GetActionType();
   
}
