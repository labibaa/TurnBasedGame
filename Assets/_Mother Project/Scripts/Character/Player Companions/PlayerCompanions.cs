using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCompanions : MonoBehaviour
{
    public Transform mainPlayer;
    public float maxDistance = 1f;
    public float maxTime = 0.8f;

    NavMeshAgent agentCompanion;
    Animator animator;
    float timer = 0f;
    bool linkUp = false;
    private void Start()
    {
        agentCompanion = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        FollowPlayer();
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
      
    }
}
