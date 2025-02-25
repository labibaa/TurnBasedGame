using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FearTacticts : ICommand
{

    CharacterBaseClasses player;
    CharacterBaseClasses target;
    TemporaryStats playerTempStats;
    TemporaryStats targetTempStats;
    ActionStat fearTactics;


    public FearTacticts(CharacterBaseClasses playerAttacker, CharacterBaseClasses targetDefender, TemporaryStats currentStatPlayer, TemporaryStats currentStatTarget, ActionStat fearTacticsScriptable)
    {
        player = playerAttacker;
        target = targetDefender;
        playerTempStats = currentStatPlayer;
        targetTempStats = currentStatTarget;
        fearTactics = fearTacticsScriptable;

    }
    public async UniTask Execute()
    {
        float actionAccuracy = fearTactics.ActionAccuracy;
        if (targetTempStats.IsDodgeActive)
        {
            actionAccuracy = actionAccuracy - 50;
        }
        if (targetTempStats.IsBlockActive)
        {
            fearTactics.BasePower = fearTactics.BasePower - 30;
        }


        if (ActionResolver.instance.ActionAccuracyCalculation(actionAccuracy))
        {

            float damage = ActionResolver.instance.CalculateDealDamage(player, target, fearTactics);
            if (targetTempStats.IsCounterActive)
            {
                playerTempStats.CurrentResolve = HealthManager.instance.ResolveCalculation(damage, targetTempStats.CurrentResolve);
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
        return "FearTacticts";
    }

    public int GetPVValue()
    {
        return fearTactics.PriorityValue;
    }

    public CharacterBaseClasses GetTarget()
    {
       return target;
    }

    public int GetAPValue()
    {
        return fearTactics.APCost;
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
}

