using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRotation : MonoBehaviour
{
    [SerializeField]
    GameObject player;


    public void LookAtPlayer()
    {
        Vector3 directionToTarget = player.transform.position - this.transform.position;

        // Create a rotation based on the direction and apply it to the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        
        transform.rotation = targetRotation;
    }
}
