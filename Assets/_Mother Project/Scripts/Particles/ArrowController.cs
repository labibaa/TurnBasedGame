using UnityEngine;
using UnityEngine.Events;

public class ArrowController : MonoBehaviour
{
    public float arrowSpeed = 5.0f;
    public string animationTrigger = "TriggerAnimation"; // Set this to the animation trigger name you want to use.
    public float destroyDelay = 1.0f; // Delay before destroying the arrow (optional).

    public bool hasReachedTarget = false;
    private Transform target;
    private float yOffset = 1;

    

    private void Update()
    {
        if (!hasReachedTarget && target != null)
        {
            Vector3 targetDirection = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Vector3 targetPosition = target.position + Vector3.up * yOffset;

            // Rotate the arrow using its local forward axis
            //transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y - 180f, 0);
            transform.rotation = targetRotation;


            // Move the arrow towards the target
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, arrowSpeed * Time.deltaTime);

            // Check if the arrow has reached the target
            if ( Mathf.Abs(transform.position.x-target.position.x)  < 0.8f)
            {
                // Arrow has reached the target, trigger the animation on the target
                //target.gameObject.GetComponent<Animator>().SetTrigger(animationTrigger);
                target.gameObject.GetComponent<Animator>().Play("Hurt");
                
                // Mark that the arrow has reached the target
                hasReachedTarget = true;

                // Destroy the arrow after a delay (optional)
                DestroyArrowAfterDelay();

                
            }
        }
    }

    private void DestroyArrowAfterDelay()
    {
        // Destroy the arrow GameObject after a delay (if specified)
        if (destroyDelay > 0)
        {
            Destroy(gameObject, destroyDelay);
        }
        else
        {
            // If no delay is specified, immediately destroy the arrow
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget ;
    }
}
