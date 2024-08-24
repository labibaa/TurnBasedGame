using UnityEngine;
using Cysharp.Threading.Tasks;

public class LerpAndLoop : MonoBehaviour
{
    public float lerpDuration = 1.0f;
    public float speed = 2f;

    private async UniTask LerpToPosition(Vector3 startPosition,Vector3 destination)
    {
        Vector3 initialPosition = startPosition;
        float startTime = Time.time;

        while (Time.time - startTime < lerpDuration)
        {
            float t = (Time.time - startTime) / lerpDuration;
            transform.position = Vector3.Lerp(initialPosition, destination, t);
            await UniTask.Yield();
        }

        // Ensure the object reaches the exact destination
        transform.position = destination;
    }

    public async UniTask MoveToDestination(Vector3 startPosition,Vector3 destination)
    {
        Vector3 directionToTarget = destination - startPosition;

        // Create a rotation based on the direction and apply it to the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        
        this.gameObject.transform.rotation = targetRotation;
        

        lerpDuration = Mathf.Max(0.5f, Mathf.Abs(startPosition.x - destination.x) / speed);
        await LerpToPosition(startPosition,destination);
        // Additional code to execute after reaching the destination can be added here.
    }
}
