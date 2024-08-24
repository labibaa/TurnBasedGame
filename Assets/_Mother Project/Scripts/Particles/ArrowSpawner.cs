using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ArrowSpawner : MonoBehaviour
{
    
    public float rotationSpeed = 5.0f; // Adjust the rotation speed as needed


    public  event Action OnFinishParticle;



    public async UniTask SpawnArrow(GameObject player, GameObject target, GameObject hitParticlePrefab)
    {
        
        GameObject arrow = Instantiate(hitParticlePrefab, player.transform.position, Quaternion.identity);
        
        ArrowController arrowController = arrow.GetComponent<ArrowController>();

        // Set the target for the arrow
        arrowController.SetTarget(target.transform);
        while (!arrowController.hasReachedTarget)
        {
            await UniTask.Yield();
        }
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        OnFinishParticle?.Invoke();

}


    public void RotatePlayerTowardsTarget(GameObject target)
    {
        if (target != null)
        {
            Debug.Log("hit");
            // Calculate the direction to the target
            Vector3 targetDirection = target.transform.position - transform.position;

            // Calculate the rotation required to look at the target
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            targetRotation.x = 0;
            targetRotation.z = 0;

            // Optionally, smoothly rotate the player towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

           
        }
    }
    public void TriggerEvent()
    {
        
        OnFinishParticle?.Invoke();
    }
}