using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveClash : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    float raycastDistance=10f;


    private void Awake()
    {
        
     agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position+new Vector3(0f,0.5f,0f), transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            // Check if the hit object has the "player" tag.
            if (hit.collider.CompareTag("Player"))
            {


               Vector2 gridPosition = GridSystem.instance.WorldToGrid(agent.GetComponent<TemporaryStats>().lastPosition.transform.position);

                agent.transform.position = GridSystem.instance._gridArray[(int)gridPosition.x,(int)gridPosition.y].transform.position;



                agent.speed = 0;
                agent.SetDestination(agent.transform.position);
                agent.isStopped = true;
                // If the hit object has the "player" tag, log a message.
                Debug.Log("Player hit: " + hit.collider.gameObject.name);
            }

            // Visualize the raycast with a debug line.
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
        }
        else
        {
            // Visualize the raycast with a debug line extending to the maximum distance.
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green);
        }

    }

}
