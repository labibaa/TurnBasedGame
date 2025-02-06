using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Buff : ICommand
{
    CharacterBaseClasses player;
    CharacterBaseClasses target;
    TemporaryStats playerTempStats;
    TemporaryStats targetTempStats;
    ImprovedActionStat buffAttack;


    public Buff(CharacterBaseClasses playerAttacker, CharacterBaseClasses targetDefender, TemporaryStats currentStatPlayer, TemporaryStats currentStatTarget, ImprovedActionStat BuffScriptable)
    {
        player = playerAttacker;
        target = targetDefender;
        playerTempStats = currentStatPlayer;
        targetTempStats = currentStatTarget;
        buffAttack = BuffScriptable;

    }



    public async UniTask Execute()
    {

        int attackOrder = checkOrder();

        float actionAccuracy = buffAttack.ActionAccuracy;

        //if (targetTempStats.IsBlockActive)
        //{
        //    greaterStrike.BasePower = greaterStrike.BasePower - 30;
        //}

        if (ActionResolver.instance.ActionAccuracyCalculation(actionAccuracy))
        {
            int diceValue = DiceNumberGenerator.instance.GetDiceValue(buffAttack.FirstPercentage, buffAttack.SecondPercentage, buffAttack.LastPercentage);
            UI.instance.SendNotification(diceValue.ToString());

            int buffPoint = Mathf.RoundToInt(ActionResolver.instance.CalculateNewDamage(diceValue, buffAttack)) * -1;
            target.DamageMultiplier = player.DamageMultiplier * 2;

            await HandleAnimation();
            UI.instance.ShowFlyingText((buffPoint * -1).ToString(), target.GetComponent<TemporaryStats>().FlyingTextParent, Color.green);
            await HealthManager.instance.PlayerMortality(targetTempStats, attackOrder);



        }
    }


    async UniTask HandleAnimation()
    {
        Transform closestTarget = TurnManager.instance.FindClosestTarget(TurnManager.instance.target, player.GetComponent<CharacterBaseClasses>());
        TempManager.instance.CharacterRotation(closestTarget.GetComponent<CharacterBaseClasses>(), player, 2f);

        /*   player.GetComponent<PlayParticle>().target = target.gameObject;
           player.GetComponent<PlayParticle>().actionSound = rangedAttack.actionSound;
           //player.GetComponent<PlayParticle>().InstantiateParticleEffect(rangedAttack.ParticleSystem);
           player.GetComponent<PlayParticle>().particlePrefab = rangedAttack.ParticleSystem;
           player.GetComponent<PlayParticle>().particlePrefabHit = rangedAttack.HitParticleSystem;
           target.GetComponent<PlayParticle>().particlePrefabHurt = rangedAttack.HurtParticleSystem;
           Debug.Log("RangedD");*/

        //CutsceneManager.instance.virtualCamera.LookAt = player.gameObject.transform;
        //CutsceneManager.instance.virtualCamera.Follow = player.gameObject.transform;
        //CutsceneManager.instance.virtualCamera.Priority = 15;

        player.GetComponent<SpawnVFX>().SetTargetAnimator(target.gameObject);
        player.GetComponent<SpawnVFX>().SetTargetVFXPosition(target.gameObject);
        player.GetComponent<SpawnVFX>().SetOwnVFXPosition(player.gameObject.GetComponent<VFXSpawnPosition>().MidBody);
        player.GetComponent<SpawnVFX>().SetVFXPrefab(buffAttack.PlayerActionVFX);
        player.GetComponent<SpawnVFX>().SetTargetHitVFXPrefab(buffAttack.TargetHitVFX);
        player.GetComponent<SpawnVFX>().SetParticle(buffAttack.particle);
        player.GetComponent<SpawnVFX>().SetVFXSound(buffAttack.actionSound);
        player.GetComponent<SpawnVFX>().SetTargetAnimation(buffAttack.TargetHurtAnimation);

        await CutsceneManager.instance.PlayAnimationForCharacter(player.gameObject, GetActionName());

        //player.GetComponent<ArrowSpawner>().SpawnArrow(player.gameObject, target.gameObject);
    }


    public string GetActionName()
    {
        return buffAttack.ActionName;
    }

    public int GetPVValue()
    {
        return buffAttack.PriorityValue;
    }

    public CharacterBaseClasses GetTarget()
    {
        return target;
    }

    public int GetAPValue()
    {
        return buffAttack.APCost;
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
