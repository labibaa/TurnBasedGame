using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Heal : ICommand
{
    CharacterBaseClasses player;
    CharacterBaseClasses target;
    TemporaryStats playerTempStats;
    TemporaryStats targetTempStats;
    ImprovedActionStat rangedAttack;


    public Heal(CharacterBaseClasses playerAttacker, CharacterBaseClasses targetDefender, TemporaryStats currentStatPlayer, TemporaryStats currentStatTarget, ImprovedActionStat rangedScriptable)
    {
        player = playerAttacker;
        target = targetDefender;
        playerTempStats = currentStatPlayer;
        targetTempStats = currentStatTarget;
        rangedAttack = rangedScriptable;

    }



    public async UniTask Execute()
    {

        int attackOrder = checkOrder();

        float actionAccuracy = rangedAttack.ActionAccuracy;
        
        //if (targetTempStats.IsBlockActive)
        //{
        //    greaterStrike.BasePower = greaterStrike.BasePower - 30;
        //}

        if (ActionResolver.instance.ActionAccuracyCalculation(actionAccuracy))
        {
            int diceValue = DiceNumberGenerator.instance.GetDiceValue(rangedAttack.FirstPercentage, rangedAttack.SecondPercentage, rangedAttack.LastPercentage);

            int healPoint = Mathf.RoundToInt(ActionResolver.instance.CalculateNewDamage(diceValue, rangedAttack) ) *-1;

            targetTempStats.CurrentHealth = HealthManager.instance.HealthCap(targetTempStats.PlayerHealth, HealthManager.instance.HealthCalculation(healPoint, targetTempStats.CurrentHealth));

            await HandleAnimation();
            UI.instance.ShowFlyingText((healPoint*-1).ToString(), target.GetComponent<TemporaryStats>().FlyingTextParent, Color.green);
            await HealthManager.instance.PlayerMortality(targetTempStats, attackOrder);
            


        }
    }


    async UniTask HandleAnimation()
    {
        Transform closestTarget = TurnManager.instance.FindClosestTarget(TurnManager.instance.target, player.GetComponent<CharacterBaseClasses>());
        TempManager.instance.CharacterRotation(closestTarget.GetComponent<CharacterBaseClasses>(), player, 2f);

        player.GetComponent<PlayParticle>().target = target.gameObject;
        player.GetComponent<PlayParticle>().actionSound = rangedAttack.actionSound;
        //player.GetComponent<PlayParticle>().InstantiateParticleEffect(rangedAttack.ParticleSystem);
        player.GetComponent<PlayParticle>().particlePrefab = rangedAttack.ParticleSystem;
        player.GetComponent<PlayParticle>().particlePrefabHit = rangedAttack.HitParticleSystem;
        target.GetComponent<PlayParticle>().particlePrefabHurt = rangedAttack.HurtParticleSystem;
        Debug.Log("RangedD");

        //CutsceneManager.instance.virtualCamera.LookAt = player.gameObject.transform;
        //CutsceneManager.instance.virtualCamera.Follow = player.gameObject.transform;
        //CutsceneManager.instance.virtualCamera.Priority = 15;

        player.GetComponent<SpawnVFX>().SetTargetAnimator(target.gameObject);
        player.GetComponent<SpawnVFX>().SetTargetVFXPosition(target.gameObject);
        player.GetComponent<SpawnVFX>().SetOwnVFXPosition(player.gameObject.GetComponent<VFXSpawnPosition>().MidBody);
        player.GetComponent<SpawnVFX>().SetVFXPrefab(rangedAttack.PlayerActionVFX);
        player.GetComponent<SpawnVFX>().SetTargetHitVFXPrefab(rangedAttack.TargetHitVFX);
        player.GetComponent<SpawnVFX>().SetParticle(rangedAttack.particle);
        player.GetComponent<SpawnVFX>().SetVFXSound(rangedAttack.actionSound);
        player.GetComponent<SpawnVFX>().SetTargetAnimation(rangedAttack.TargetHurtAnimation);

        await CutsceneManager.instance.PlayAnimationForCharacter(player.gameObject, GetActionName());

        //player.GetComponent<ArrowSpawner>().SpawnArrow(player.gameObject, target.gameObject);
    }


    public string GetActionName()
    {
        return rangedAttack.ActionName;
    }

    public int GetPVValue()
    {
        return rangedAttack.PriorityValue;
    }

    public CharacterBaseClasses GetTarget()
    {
        return target;
    }

    public int GetAPValue()
    {
        return rangedAttack.APCost;
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
