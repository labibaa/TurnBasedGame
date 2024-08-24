using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaptivatingPerformance : ICommand
{

    CharacterBaseClasses player;
    CharacterBaseClasses target;
    TemporaryStats playerTempStats;
    TemporaryStats targetTempStats;
    ActionStat captivatingPerformance;
    

    public CaptivatingPerformance(CharacterBaseClasses playerAttacker, CharacterBaseClasses targetDefender, TemporaryStats currentStatPlayer, TemporaryStats currentStatTarget, ActionStat captivatingScriptable)
    {
        player = playerAttacker;
        target = targetDefender;
        playerTempStats = currentStatPlayer;
        targetTempStats = currentStatTarget;
        captivatingPerformance = captivatingScriptable;

    }
    public async UniTask Execute()
    {
       
        float actionAccuracy = captivatingPerformance.ActionAccuracy;
        if (targetTempStats.IsDodgeActive)
        {
            actionAccuracy = actionAccuracy - 50;
        }
        if (targetTempStats.IsBlockActive)
        {
            captivatingPerformance.BasePower = captivatingPerformance.BasePower - 30;
        }


        if (ActionResolver.instance.ActionAccuracyCalculation(actionAccuracy)) {
            
            float damage = ActionResolver.instance.CalculateDealDamage(player, target, captivatingPerformance);
            if (targetTempStats.IsCounterActive)
            {
                playerTempStats.CurrentResolve = HealthManager.instance.ResolveCalculation(damage, playerTempStats.CurrentResolve);
            }
            else
            {
                targetTempStats.CurrentResolve = HealthManager.instance.ResolveCalculation(damage, targetTempStats.CurrentResolve);
            }
           
            Debug.Log("Damage done");
        }
    }

    public string GetActionName()
    {
        return "CaptivatingPerformance";
    }

    public int GetPVValue()
    {
        return captivatingPerformance.PriorityValue;
    }

    public CharacterBaseClasses GetTarget()
    {
        return target;
    }
    public int GetAPValue()
    {
        return captivatingPerformance.APCost;
    }

    public NavMeshAgent GetAgent()
    {
        throw new System.NotImplementedException();
    }

    public List<GameObject> GetPaths()
    {
        throw new System.NotImplementedException();
    }

    public string GetActionType()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update

}
