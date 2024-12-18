using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoneShield: ICommand
{
    CharacterBaseClasses player;
    CharacterBaseClasses target;
    TemporaryStats playerTempStats;
    TemporaryStats targetTempStats;
    ImprovedActionStat boneShield;


    public BoneShield(CharacterBaseClasses playerAttacker, CharacterBaseClasses targetDefender, TemporaryStats currentStatPlayer, TemporaryStats currentStatTarget, ImprovedActionStat boneShieldScriptable)
    {
        player = playerAttacker;
        target = targetDefender;
        playerTempStats = currentStatPlayer;
        targetTempStats = currentStatTarget;
        boneShield = boneShieldScriptable;

    }







    public async UniTask Execute()
    {

       

        if (ActionResolver.instance.ActionAccuracyCalculation(boneShield.ActionAccuracy)&& player.gameObject.activeInHierarchy)
        {


            int healPoint = -10;

          

            
            
                targetTempStats.CurrentHealth = HealthManager.instance.HealthCap(targetTempStats.PlayerHealth, HealthManager.instance.HealthCalculation(healPoint, targetTempStats.CurrentHealth));

                await HandleAnimation();
               
            


        }
    }


    async UniTask HandleAnimation()
    {
        Transform closestTarget = TurnManager.instance.FindClosestTarget(TurnManager.instance.target, player.GetComponent<CharacterBaseClasses>());
        TempManager.instance.CharacterRotation(closestTarget.GetComponent<CharacterBaseClasses>(), player, 2f);

        //player.GetComponent<PlayParticle>().target = target.gameObject;
        //player.GetComponent<PlayParticle>().actionSound = rangedAttack.actionSound;
        ////player.GetComponent<PlayParticle>().InstantiateParticleEffect(rangedAttack.ParticleSystem);
        //player.GetComponent<PlayParticle>().particlePrefab = rangedAttack.ParticleSystem;
        //player.GetComponent<PlayParticle>().particlePrefabHit = rangedAttack.HitParticleSystem;
        //target.GetComponent<PlayParticle>().particlePrefabHurt = rangedAttack.HurtParticleSystem;
        Debug.Log("RangedD");

        //CutsceneManager.instance.virtualCamera.LookAt = player.gameObject.transform;
        //CutsceneManager.instance.virtualCamera.Follow = player.gameObject.transform;
        //CutsceneManager.instance.virtualCamera.Priority = 15;

        //player.GetComponent<SpawnVFX>().SetTargetAnimator(target.gameObject);
        //player.GetComponent<SpawnVFX>().SetTargetVFXPosition(target.gameObject);
        //player.GetComponent<SpawnVFX>().SetOwnVFXPosition(player.gameObject.GetComponent<VFXSpawnPosition>().MidBody);
        

        await CutsceneManager.instance.PlayAnimationForCharacter(player.gameObject, GetActionName());

        //player.GetComponent<ArrowSpawner>().SpawnArrow(player.gameObject, target.gameObject);
    }


    public string GetActionName()
    {
        return "BoneShield";
    }

    public int GetPVValue()
    {
        return boneShield.PriorityValue;
    }

    public CharacterBaseClasses GetTarget()
    {
        return player;
    }

    public int GetAPValue()
    {
        return boneShield.APCost;
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
        return "Ranged";
    }
   
}
