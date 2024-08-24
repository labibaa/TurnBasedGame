using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VenomCloud : ICommand
{
    CharacterBaseClasses player;
    CharacterBaseClasses target;
    TemporaryStats playerTempStats;
    TemporaryStats targetTempStats;
    ImprovedActionStat venomAttack;
    int gridSizeX;
    int gridSizeY;

    public VenomCloud(CharacterBaseClasses playerAttacker, CharacterBaseClasses targetDefender, TemporaryStats currentStatPlayer, TemporaryStats currentStatTarget, ImprovedActionStat venomScriptable) {

        player = playerAttacker;
        target = targetDefender;
        playerTempStats = currentStatPlayer;
        targetTempStats = currentStatTarget;
        venomAttack = venomScriptable;
        gridSizeX = GridSystem.instance._gridArray.GetLength(0);
        gridSizeY = GridSystem.instance._gridArray.GetLength(1);
    }
    public async UniTask Execute()
    {
       // int attackOrder = checkOrder();


        float actionAccuracy = venomAttack.ActionAccuracy;
        if (ActionResolver.instance.ActionAccuracyCalculation(actionAccuracy))
        {

            Debug.Log("VenomExecuted");
            Vector2 playerCoordinate = GridSystem.instance.WorldToGrid(player.transform.position);
            List<Vector2> TilesToDeployGas = GridMovement.instance.GetAdjacentNeighbors(playerCoordinate, venomAttack.ActionRange);
            Debug.Log("Venom Count" + TilesToDeployGas.Count);
            for (int i = 0; i < TilesToDeployGas.Count; i++)
            {

                if ((TilesToDeployGas[i].x >= 0 && TilesToDeployGas[i].x < gridSizeX) && (TilesToDeployGas[i].y >= 0 && TilesToDeployGas[i].y < gridSizeY))
                {

                    GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].GetComponent<VenomEffector>().HasEffect = true;
                    GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].GetComponent<VenomEffector>().EffectOwner = playerTempStats;
                    GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].GetComponent<VenomEffector>().Venom = venomAttack;
                    GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].GetComponent<VenomEffector>().TurnCount = venomAttack.PriorityValue;//(actually DOT round)
                    GridSystem.instance._gridArray[(int)TilesToDeployGas[i].x, (int)TilesToDeployGas[i].y].GetComponent<VenomEffector>().GridPosition = TilesToDeployGas[i];
                }
            }
            await HandleAnimation();
        }
    }
    public async UniTask HandleAnimation()
    {
        player.GetComponent<SpawnVFX>().SetOwnVFXPosition(player.GetComponent<VFXSpawnPosition>().CharacterBodyPosition[venomAttack.CharacterBodyLocation]);
        player.GetComponent<SpawnVFX>().SetVFXPrefab(venomAttack.PlayerActionVFX);
        player.GetComponent<SpawnVFX>().SetTargetHitVFXPrefab(venomAttack.TargetHitVFX);
        player.GetComponent<SpawnVFX>().SetParticle(venomAttack.particle);
        player.GetComponent<SpawnVFX>().SetVFXSound(venomAttack.actionSound);
        player.GetComponent<SpawnVFX>().SetTargetAnimation(venomAttack.TargetHurtAnimation);

        await CutsceneManager.instance.PlayAnimationForCharacter(player.gameObject, GetActionName());
    }
    public string GetActionName()
    {
        return venomAttack.ActionName;
    }

    public int GetPVValue()
    {
        return venomAttack.PriorityValue;
    }

    public CharacterBaseClasses GetTarget()
    {
        return target;
    }

    public int GetAPValue()
    {
        return venomAttack.APCost;
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
