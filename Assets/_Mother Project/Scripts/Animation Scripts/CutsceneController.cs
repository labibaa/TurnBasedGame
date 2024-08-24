using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public Animator playerAnimator; // This could be any character's Animator
    public string trackName = "CharacterAnimationTrack"; // Name of the track in the Timeline

    void Start()
    {
        // Find the track by name
        TimelineAsset timelineAsset = (TimelineAsset)playableDirector.playableAsset;
        foreach (TrackAsset track in timelineAsset.GetOutputTracks())
        {
            if (track.name == trackName)
            {
                // Set the playerAnimator as the binding for the animation track
                playableDirector.SetGenericBinding(playerAnimator, track);
                break;
            }
        }

        // Play the timeline
        playableDirector.Play();
    }
}
