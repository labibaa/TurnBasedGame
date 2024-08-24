
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnVFX : MonoBehaviour
{
    // public static SpawnVFX instance;

    public static event Action hitAnimation;
    public static event Action targetLessVfx;

    public List<VisualEffect> effectPrefabList;
    public List<VisualEffect> hitEffectPrefabList;
   
    public List<Transform> hitEffectTransformList;
    public List<Transform> effectTransformList;
    public List<Transform> targetHitTransformList;

    public List<ParticleSystem> particles;
    public GameObject TrailVisual;

    public float speed = 5f;
    public Animator charAnimator;
    public Animator targetAnimator;

    public List<String> animationList;
    public int animationIndex;
   
    bool startLerp;
    public bool isShieldUp;

    VisualEffect effect;
    VisualEffect hiteffect;
    GameObject playerPosition;
    GameObject targetPosition;
    GameObject target;
    ParticleSystem particle;
    String targetAnimationName;

    AudioClip vfxAudioClip;

    void Awake()
    {
        //if(instance == null)
        //{
        //    instance = this;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           // AnimationPlay(animationList, charAnimator,animationIndex);

        }
     
    }
    private void OnEnable()
    {
        GridSystem.OnGridGenerationSpawn += CharacterTrailVisualOn;
    }

    private void OnDisable()
    {
        GridSystem.OnGridGenerationSpawn -= CharacterTrailVisualOn;
    }
    public void SetTargetAnimator(GameObject TargetAnimator)
    {
        target = TargetAnimator;
    }
    public void SetTargetAnimation(String Animation)
    {
        targetAnimationName = Animation;
    }
    public void SetOwnVFXPosition(GameObject vfxPosition)
    {
        playerPosition = vfxPosition;
    }
    public void SetTargetVFXPosition(GameObject TargetVfxPosition)
    {
        targetPosition = TargetVfxPosition;
    }
    public void SetVFXPrefab(VisualEffect effectVfxPrefab)
    {
        effect = effectVfxPrefab;
    }

    public void SetTargetHitVFXPrefab(VisualEffect targetHitEffectVfxPrefab)
    {
        hiteffect = targetHitEffectVfxPrefab;
    }

    public void SetParticle(ParticleSystem newParticle)
    {
        particle = newParticle;
    }
    public void SetVFXSound(AudioClip newVFXSound)
    {
        vfxAudioClip = newVFXSound;
    }
    public void PlayParticles()
    {
        StartCoroutine( SpawnPlayerParticle(particle, targetPosition.transform));
    }

    public IEnumerator SpawnPlayerParticle(ParticleSystem particleEffect, Transform particleSpawnPosition)
    {
        ParticleSystem particleSystem = Instantiate(particleEffect, particleSpawnPosition.position, particleEffect.transform.rotation, particleSpawnPosition);

        particleSystem.Play();
        yield return new WaitForSeconds(2f);
        Destroy(particleSystem.gameObject);

    }

    public void PlayerVFX() //in parameter player transform, visual effect effectPrefab
    {
      // Transform vfxSpawnPosition =  player.GetComponent<VFXSpawnPosition>().LowerBody.transform;
        Transform vfxSpawnPosition = playerPosition.transform;

        PlayPlayerVFX(effect, vfxSpawnPosition);
    }

    public void PlayPlayerVFX(VisualEffect effectPrefab, Transform effectPosition) //in parameter player transform, visual effect effectPrefab
    {
        effect = Instantiate(effectPrefab, effectPosition.position, effectPrefab.transform.rotation, effectPosition.transform);

        effect.SendEvent("create");
        //effect.playRate = 1f;
        Debug.Log("effect");
    }

    public void PlayTargetVFX(VisualEffect hitEffectPrefab, Transform hitEffectPosition, Transform targetHitPosition, GameObject target)
    {
        hiteffect = Instantiate(hitEffectPrefab, hitEffectPosition.position, Quaternion.identity);
        //hiteffect.transform.localPosition = Vector3.zero;
        //hiteffect.transform.localRotation = Quaternion.identity;
        //hiteffect.transform.localScale = Vector3.one;

        hiteffect.SendEvent("create");
        // hiteffect.playRate = .2f;
        ThrowVFX throwVFX = hiteffect.GetComponent<ThrowVFX>();
        throwVFX.SetTargetVFX(targetHitPosition);
        throwVFX.SetTargetAnimator(target);

        CurveThrowVFX curveThrowVFX = hiteffect.GetComponent<CurveThrowVFX>();
        curveThrowVFX.SetCurveTargetVFX(targetHitPosition);
        curveThrowVFX.SetTargetAnimator(target);
    }
    public void targetVFX() //in parameter target transform
    {
        Transform hitVfxSpawnPosition = playerPosition.transform;
        Transform targetHitPosition = targetPosition.transform;
        PlayTargetVFX(hiteffect, hitVfxSpawnPosition, targetHitPosition,target);

    }



    public void MoveHitVfx()
    {
        hiteffect.transform.parent = null;
        hiteffect.GetComponent<ThrowVFX>().enabled = true;
    }

    public void CurveMoveHit()
    {
        hiteffect.transform.parent = null;
        hiteffect.GetComponent<CurveThrowVFX>().enabled = true;
        hiteffect.GetComponent<CurveThrowVFX>().tr();
        Debug.Log("gg");
    }

    public void targetLess()
    {
        ShieldVfx();
    }

    public IEnumerator ShieldVfx()
    {
        yield return new WaitForSeconds(1f);
        effect.SendEvent("end");

    }

    public void PlayHitEffect()
    {

        //Hiteffect.transform.position = Vector3.MoveTowards(Hiteffect.transform.position, target.position, speed * Time.deltaTime);
        //if (Hiteffect.transform.position == target.position)
        //{
        //    Hiteffect.SendEvent("hit");
        //    startLerp = false;
        //    // targetAnimator.Play("Damage1");
        //    AnimationPlay("Damage1", targetAnimator);
        //    hitTrigger?.Invoke();
        //}

        if (isShieldUp)
        {
            Debug.Log("in");
            targetLessVfx?.Invoke();
        }
    }

    public void HitAnimation()
    {

        //AnimationPlay(animationList, targetAnimator, 12);
        Debug.Log(target);
        TriggerNextTurn();
        AnimationPlay(targetAnimationName, target.GetComponent<Animator>());
    }
    public void HitVfx()
    {

        EndVfx(effect);
       // StartCoroutine(DestroyVFX(effect,hiteffect));
    }

   
    public void EndVfx(VisualEffect effect)
    {
        Debug.Log("EndVFX");
        if(effect != null)
        {
            Debug.Log("end");
            effect.SendEvent("end");
            effect.SendEvent("stop");
            StartCoroutine(DestroyVFX(effect));
        }

    }

    public void VFXSpeedRate(float rate)
    {
        effect.playRate = rate;
    }
    public void animSpeedIncrease(float sp)
    {
        charAnimator.speed = sp;
    }

    public IEnumerator DestroyVFX(VisualEffect desEffect)
    {
        yield return new WaitForSeconds(1f);
        Destroy(desEffect.gameObject);
      //  Destroy(hiteffect.gameObject);
        Debug.Log("destroyt");
    }
    public void PlayVFXSound()
    {
        AudioController.instance.PlaySound(vfxAudioClip);
    }



    public void AnimationPlay(String animationName, Animator animator)
    {
        animator.Play(animationName);
    }

    public void TriggerNextTurn()
    {
        hitAnimation?.Invoke();
    }

    public void CharacterTrailVisualOn()
    {
        if (TrailVisual)
        {
            TrailVisual.SetActive(true);
        }
    }
}