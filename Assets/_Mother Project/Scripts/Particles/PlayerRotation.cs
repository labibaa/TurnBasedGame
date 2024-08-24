using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
   
    public float rotationSpeed = 5.0f; // Adjust the rotation speed as needed

    public void RotatePlayerTowardsTarget(GameObject target)
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 targetDirection = target.transform.position - transform.position;

            // Calculate the rotation required to look at the target
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            targetRotation.x = 0;
            targetRotation.z = 0;

            // Optionally, smoothly rotate the player towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if the rotation is close enough to the target rotation
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f)
            {
                // Play your animation here or trigger it as needed
            }
        }
    }
}
