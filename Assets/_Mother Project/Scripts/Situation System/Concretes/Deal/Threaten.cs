using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Threaten : ICommand
{
    CharacterBaseClasses player;
    CharacterBaseClasses target;
    TemporaryStats playerTempStats;
    TemporaryStats targetTempStats;
    ActionStat threaten;

    public Threaten(CharacterBaseClasses playerAttacker, CharacterBaseClasses targetDefender, TemporaryStats currentStatPlayer, TemporaryStats currentStatTarget, ActionStat threatenScriptable)
    {
        player = playerAttacker;
        target = targetDefender;
        playerTempStats = currentStatPlayer;
        targetTempStats = currentStatTarget;
        threaten = threatenScriptable;

    }



    public async UniTask Execute()
    {
        float actionAccuracy = threaten.ActionAccuracy;
        if (targetTempStats.IsDodgeActive)
        {
            actionAccuracy = actionAccuracy - 50;
        }
        if (targetTempStats.IsBlockActive)
        {
            threaten.BasePower = threaten.BasePower - 30;
        }

        if (ActionResolver.instance.ActionAccuracyCalculation(actionAccuracy)) {

            Debug.Log("Threaten executed");


            float damage = ActionResolver.instance.CalculateDealDamage(player, target, threaten);
            if (targetTempStats.IsCounterActive)
            {
                playerTempStats.CurrentResolve = HealthManager.instance.ResolveCalculation(damage, targetTempStats.CurrentResolve);
            }
            else
            {
                targetTempStats.CurrentResolve = HealthManager.instance.ResolveCalculation(damage, targetTempStats.CurrentResolve);
            }

            //bool isadjacent = GridMovement.instance.InAdjacentMatrix(player.gameObject.transform.position, target.gameObject.transform.position, threaten.ActionRange);
            //if (isadjacent)
            //{

            //    float damage = ActionResolver.instance.CalculateDealDamage(player, target, threaten);
            //    targetTempStats.CurrentResolve = HealthManager.instance.ResolveCalculation(damage, targetTempStats.CurrentResolve);

            //}
        }
    }

    public string GetActionName()
    {
        return "Threaten";
    }

    public int GetPVValue()
    {
        return threaten.PriorityValue;
    }

    public CharacterBaseClasses GetTarget()
    {
        return target;
    }

    public int GetAPValue()
    {
        return threaten.APCost;
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
