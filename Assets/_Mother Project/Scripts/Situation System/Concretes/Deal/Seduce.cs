using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seduce : ICommand
{

    CharacterBaseClasses player;
    CharacterBaseClasses target;
    TemporaryStats playerTempStats;
    TemporaryStats targetTempStats;
    ActionStat seduce;


    public Seduce(CharacterBaseClasses playerAttacker, CharacterBaseClasses targetDefender, TemporaryStats currentStatPlayer, TemporaryStats currentStatTarget, ActionStat seduceScriptable)
    {
        player = playerAttacker;
        target = targetDefender;
        playerTempStats = currentStatPlayer;
        targetTempStats = currentStatTarget;
        seduce = seduceScriptable;

    }


    public async UniTask Execute()
    {
        float actionAccuracy = seduce.ActionAccuracy;
        if (targetTempStats.IsDodgeActive)
        {
            actionAccuracy = actionAccuracy - 50;
        }
        if (targetTempStats.IsBlockActive)
        {
            seduce.BasePower = seduce.BasePower - 30;
        }


        if (ActionResolver.instance.ActionAccuracyCalculation(actionAccuracy))
        {

            float damage = ActionResolver.instance.CalculateDealDamage(player, target, seduce);
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
        return "Seduce";
    }

    public int GetPVValue()
    {
        return seduce.PriorityValue;
    }

    public CharacterBaseClasses GetTarget()
    {
       return target;
    }

    public int GetAPValue()
    {
        return seduce.APCost;
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
