using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    // Reference to the Timeline asset
    public PlayableDirector timeline;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that triggered the event has a specific tag or is a specific object
        // This ensures that the timeline only plays when certain objects collide with the cube
        if (other.CompareTag("Player")) // Change "YourTag" to the desired tag
        {
            // If the collider's tag matches, play the Timeline
            if (timeline != null)

            {
                timeline.Play();
            }
        }
    }
}
