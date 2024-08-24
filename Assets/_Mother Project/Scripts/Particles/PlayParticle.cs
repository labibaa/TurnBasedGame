using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayParticle : MonoBehaviour
{
    public Animator animator;
    private Vector3 leftHandPosition;
    private Vector3 leftHandThumbPosition, headPosition;
    public GameObject particlePrefab;
    public GameObject particlePrefabHit;
    public GameObject particlePrefabHurt;
    public GameObject target;
    public AudioClip actionSound;
    ArrowSpawner _arrowSpawner;
    Transform leftHand;
    Transform leftHandThumb;
    Transform head;

   
   
    private void Update()
    {
        if (animator != null)
        {
            leftHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
            leftHandPosition = leftHand.position;

            leftHandThumb = animator.GetBoneTransform(HumanBodyBones.LeftThumbProximal);
            leftHandThumbPosition = leftHandThumb.position;

            head = animator.GetBoneTransform(HumanBodyBones.Head);
            headPosition = head.position;
        }
    }




  
    void PlayParticleEffectMelee()
    {
        particlePrefab = Instantiate(particlePrefab, transform.position, transform.rotation);
        ParticleSystem particles = particlePrefab.GetComponent<ParticleSystem>();
        particles.Play();
        AudioController.instance.PlaySound(actionSound);
        SpawnParticle();
        
    }


    void PlayParticleEffectWithNoTarget()
    {
        particlePrefab = Instantiate(particlePrefab, transform.position, transform.rotation);
        ParticleSystem particles = particlePrefab.GetComponent<ParticleSystem>();
        particles.Play();
        AudioController.instance.PlaySound(actionSound);
        _arrowSpawner = GetComponent<ArrowSpawner>();
        _arrowSpawner.TriggerEvent();
    }

    void PlayParticleEffectRanged()
    {
        particlePrefab = Instantiate(particlePrefab, transform.position, transform.rotation);
        ParticleSystem particles = particlePrefab.GetComponent<ParticleSystem>();
        particles.Play();
        AudioController.instance.PlaySound(actionSound);
        SpawnParticle();

    }

    void PlayParticleEffectTarget()
    {
        particlePrefab = Instantiate(particlePrefabHurt, target.transform.position, Quaternion.identity);
        ParticleSystem particles = particlePrefab.GetComponent<ParticleSystem>();
        particles.Play();

    }

    public async UniTask SpawnParticle()
    {

        _arrowSpawner = GetComponent<ArrowSpawner>();


        await _arrowSpawner.SpawnArrow(leftHand.gameObject,target, particlePrefabHit);
       
    }
}
