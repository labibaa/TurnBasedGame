using System.Collections;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    // Array to store character names and corresponding animation names
    public CharacterAnimationPair[] characterAnimations;

    // Delay between playing animations
    public float delayBetweenAnimations = 3f;

    // Reference to the Animator component of the characters
    private Animator[] animators;

    private void Start()
    {
        // Get the Animator components from each character GameObject
        animators = new Animator[characterAnimations.Length];
        for (int i = 0; i < characterAnimations.Length; i++)
        {
            animators[i] = GameObject.Find(characterAnimations[i].characterName).GetComponent<Animator>();
        }

        // Start playing animations after a delay
        StartCoroutine(PlayAnimationsWithDelay());
    }

    private IEnumerator PlayAnimationsWithDelay()
    {
        foreach (CharacterAnimationPair characterAnimation in characterAnimations)
        {
            // Find the index of the corresponding Animator
            int animatorIndex = System.Array.FindIndex(animators, animator =>
                animator.gameObject.name == characterAnimation.characterName);

            // Check if the character exists and has an Animator component
            if (animatorIndex >= 0 && animators[animatorIndex] != null)
            {
                // Play the animation with the given animation name

                animators[animatorIndex].SetTrigger(characterAnimation.animationName);

                // Wait for the specified delay before playing the next animation
                yield return new WaitForSeconds(delayBetweenAnimations);
            }
            else
            {
                Debug.LogError("Character or Animator not found for " + characterAnimation.characterName);
            }
        }
    }
}

[System.Serializable]
public class CharacterAnimationPair
{
    public string characterName;
    public string animationName;
}
