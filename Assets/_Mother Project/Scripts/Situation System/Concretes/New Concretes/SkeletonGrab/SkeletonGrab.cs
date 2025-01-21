using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class SkeletonGrab : ICommand
{
    CharacterBaseClasses player;
    CharacterBaseClasses target;
    TemporaryStats playerTempStats;
    TemporaryStats targetTempStats;
    ImprovedActionStat skeletonGrab;
    int gridSizeX;
    int gridSizeY;

    public SkeletonGrab(CharacterBaseClasses playerAttacker, CharacterBaseClasses targetDefender, TemporaryStats currentStatPlayer, TemporaryStats currentStatTarget, ImprovedActionStat skeletonGrabScriptable)
    {

        player = playerAttacker;
        target = targetDefender;
        playerTempStats = currentStatPlayer;
        targetTempStats = currentStatTarget;
        skeletonGrab = skeletonGrabScriptable;
        gridSizeX = GridSystem.instance._gridArray.GetLength(0);
        gridSizeY = GridSystem.instance._gridArray.GetLength(1);
    }
    public async UniTask Execute()
    {
        Debug.Log("SkeletonGrabExecuted");
        Vector2 playerCoordinate = GridSystem.instance.WorldToGrid(player.transform.position);
        List<Vector2> TilesToDeployGas = GridMovement.instance.GetAdjacentNeighbors(playerCoordinate, skeletonGrab.ActionRange);
        Debug.Log("SkeletonGrab Count" + TilesToDeployGas.Count);
        for (int i = 0; i < TilesToDeployGas.Count; i++)
        {

            if ((TilesToDeployGas[i].x >= 0 && TilesToDeployGas[i].x < gridSizeX) && (TilesToDeployGas[i].y >= 0 && TilesToDeployGas[i].y < gridSizeY))
            {
                Debug.Log("Executed");
                GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].GetComponent<EffectorSkeletonjGrab>().HasEffect = true;
                GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].GetComponent<EffectorSkeletonjGrab>().EffectOwner = playerTempStats;
                GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].GetComponent<EffectorSkeletonjGrab>().SkeletonGrab_IAS = skeletonGrab;
                GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].GetComponent<EffectorSkeletonjGrab>().TurnCount = skeletonGrab.PriorityValue;//(actually DOT round)
                GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].GetComponent<EffectorSkeletonjGrab>().GridPosition = TilesToDeployGas[i];
               // GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].GetComponent<EffectorSkeletonjGrab>().SmokeObject = OrbSpawner.instance.SpawnSmoke(GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].transform);
            }
        }
        await HandleAnimation();

    }
    public async UniTask HandleAnimation()
    {
        player.GetComponent<SpawnVFX>().SetOwnVFXPosition(player.GetComponent<VFXSpawnPosition>().CharacterBodyPosition[skeletonGrab.CharacterBodyLocation]);
        player.GetComponent<SpawnVFX>().SetVFXPrefab(skeletonGrab.PlayerActionVFX);
        player.GetComponent<SpawnVFX>().SetTargetHitVFXPrefab(skeletonGrab.TargetHitVFX);
        player.GetComponent<SpawnVFX>().SetParticle(skeletonGrab.particle);
        player.GetComponent<SpawnVFX>().SetVFXSound(skeletonGrab.actionSound);
        player.GetComponent<SpawnVFX>().SetTargetAnimation(skeletonGrab.TargetHurtAnimation);

        await CutsceneManager.instance.PlayAnimationForCharacter(player.gameObject, GetActionName());
    }
    public string GetActionName()
    {
        return skeletonGrab.ActionName;
    }

    public int GetPVValue()
    {
        return skeletonGrab.PriorityValue;
    }

    public CharacterBaseClasses GetTarget()
    {
        return target;
    }

    public int GetAPValue()
    {
        return skeletonGrab.APCost;
    }

    public NavMeshAgent GetAgent()
    {
        return null;
    }

    public List<GameObject> GetPaths()
    {
        return null;
    }
    public string GetActionType()
    {
        return "Ranged";
    }
    int checkOrder()
    {
        return TurnManager.instance.players.IndexOf(target.GetComponent<PlayerTurn>()) - TurnManager.instance.players.IndexOf(player.GetComponent<PlayerTurn>());

    }
}
