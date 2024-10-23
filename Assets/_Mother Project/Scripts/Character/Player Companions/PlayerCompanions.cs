using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerCompanions : MonoBehaviour
{
    public Transform mainPlayer;
    public float maxDistance = 1f;
    public float maxTime = 0.8f;
    float timer = 0f;
    bool linkUp = false;
    bool isMainCharacter;

    NavMeshAgent agentCompanion;
    Animator animator;
    ThirdPersonController thirdPersonController;
    PlayerInput playerInput;
 
    private void Start()
    {
        agentCompanion = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetFloat("MotionSpeed", 1);
        thirdPersonController = GetComponent<ThirdPersonController>();
        playerInput = GetComponent<PlayerInput>();

    }
    private void Update()
    {
        LinkUp();
        FollowPlayer();
        SwitchPlayer();


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

    public void SwitchPlayer()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMainCharacter = !isMainCharacter; // Toggle between characters
            CharacterSwitch(); // Apply the switch

        }
        
    }
    public void CharacterSwitch()
    {
        if (isMainCharacter)
        {
            thirdPersonController.enabled = true;  // Enable movement
            playerInput.enabled = true;           // Enable input
            agentCompanion.enabled = false;       // Disable NavMesh when main character
        }
        else
        {
            thirdPersonController.enabled = false; // Disable movement
            playerInput.enabled = false;          // Disable input
            agentCompanion.enabled = true;        // Enable NavMesh for companion
        }
    }
}
