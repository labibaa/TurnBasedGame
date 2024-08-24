using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;
using System;

public class PlayVFX : MonoBehaviour
{
    public static event Action startVfx;
    public static event Action targetLessVfx;

    public VisualEffect effect; 
    public VisualEffect Hiteffect;
    public Transform target; 
    public float speed = 5f;
    public Animator charAnimator;
    public Animator targetAnimator;
    public String animationName;
    bool startLerp;
    public bool isShieldUp;


    private void OnEnable()
    {
        startVfx += PlayerVFX;
       // triggerHitVfx += PlayHitEffect;
        ThrowVFX.hitTrigger += HitVfx;
        targetLessVfx += targetLessVfx;
    }
    private void OnDisable()
    {
        startVfx -= PlayerVFX;
        //triggerHitVfx -= PlayHitEffect;
        ThrowVFX.hitTrigger -= HitVfx;
        targetLessVfx -= targetLessVfx;
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    // Send the event to the Visual Effect Graph
        //    effect.SendEvent("create");
        //    transform.position = Vector3.MoveTowards(transform.position,  effectPrefab.position, speed * Time.deltaTime);   
        //    if(transform.position == effectPrefab.position)
        //    {
        //        effect.SendEvent("hit");
        //    }

        //}
        if (Input.GetKeyDown(KeyCode.L))
        {

             startVfx?.Invoke();
            AnimationPlay(animationName, charAnimator);

            startLerp = true;
           // PlayVFXxx();
            
        }
        if (startLerp)
        {
            PlayHitEffect();
        }
        else
        {
          //  StartCoroutine(EndVfx());
        }
    }

    public void PlayerVFX()
    {

        //charAnimator.Play("Magic_Jump");
        
        effect.SendEvent("create");
        targetVFX();
        effect.playRate = 0.4f;
        
        //triggerHitVfx?.Invoke();

    }

    public void targetVFX ()
    {
        Hiteffect.SendEvent("create");

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

        Hiteffect.transform.position = Vector3.MoveTowards(Hiteffect.transform.position, target.position, speed * Time.deltaTime);
        if (Hiteffect.transform.position == target.position)
        {
            Hiteffect.SendEvent("hit");
            startLerp = false;
            // targetAnimator.Play("Damage1");
            AnimationPlay("Damage1", targetAnimator);
            HitVfx();
        }

        if (isShieldUp)
        {
            Debug.Log("in");
            targetLessVfx?.Invoke();
        }
    }

    public void HitVfx()
    {

       StartCoroutine (EndVfx());
      
    }
    public IEnumerator EndVfx()
    {
        AnimationPlay("Damage1", targetAnimator);
        yield return new WaitForSeconds(0.5f);
        effect.SendEvent("end");
        effect.SendEvent("stop");
        Hiteffect.SendEvent("end");
    }

    public void AnimationPlay(string animationName,Animator animator)
    {
        animator.Play(animationName);
    }

}
