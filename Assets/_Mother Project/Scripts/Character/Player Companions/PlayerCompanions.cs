using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerCompanions : MonoBehaviour
{
    public Transform mainPlayer;
    public float maxDistance = 1f;
    public float maxTime = 0.8f;
    float timer = 0f;
    [SerializeField]bool linkUp = false;

    NavMeshAgent agentCompanion;
    Animator animator;

    private void OnDisable()
    {
        linkUp = false;
        timer = maxTime;
        if (animator != null)
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("MotionSpeed", 1);
        }
    }
    private void OnEnable()
    {
        // Reset parameters when the component is re-enabled
        linkUp = false;
        timer = maxTime;
        if (animator != null)
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("MotionSpeed", 1);
        }
    }
    private void Start()
    {
        agentCompanion = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("MotionSpeed", 1);
        }

    }
    private void Update()
    {
        LinkUp();
        FollowPlayer();

    }
    public void LinkUp()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!linkUp)
            {
                linkUp = true;
            }
            else
            {
                linkUp = false;
                animator.SetFloat("Speed", 0);
                agentCompanion.ResetPath();
            }
        }
    }

    public void FollowPlayer()
    {
        if (linkUp)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                if ((mainPlayer.position - agentCompanion.destination).sqrMagnitude > maxDistance * maxDistance)
                {
                    agentCompanion.SetDestination(mainPlayer.position);
                }
                timer = maxTime;
            }
            animator.SetFloat("Speed", agentCompanion.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            agentCompanion.ResetPath();
        }
      
    }

 
}
