using Cinemachine;
using UnityEngine;

public class ConfinerSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;  // Assign your Cinemachine Virtual Camera here
    public BoxCollider roomCollider;               // Assign the first BoxCollider for Room 1

   
    private CinemachineConfiner confiner;

    void Start()
    {
        // Get the CinemachineConfiner component from the virtual camera
        confiner = virtualCamera.GetComponent<CinemachineConfiner>();
        
    }

    // Function to change the confiner dynamically
    public void SetConfiner(BoxCollider newCollider)
    {
        // Update the confiner's bounding volume with the new collider
        confiner.m_BoundingVolume = newCollider;

        // Invalidate the confiner's path cache to refresh the bounds
        confiner.InvalidatePathCache();
    }

    // Use trigger enter to switch between the colliders
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            SetConfiner(roomCollider);
        }
            
        
    }


}
