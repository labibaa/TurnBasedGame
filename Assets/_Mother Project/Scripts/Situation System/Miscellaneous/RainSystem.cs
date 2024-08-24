using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystem : MonoBehaviour
{
    public ParticleSystem particle; // Reference to the particle system

    private float timer = 0.0f; // Timer to keep track of the cycle time
    [SerializeField] float cycleTime; // in minutes
    bool isParticleActive = false;









    AudioSource audioSource;
    public float fadeTime = 1.0f;
    private float initialVolume;



    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Store the initial volume of the audio source
        initialVolume = audioSource.volume;
    }








    public void StopAudioWithFadeOut()
    {
        StartCoroutine(FadeOutAudioCoroutine());
    }

    private IEnumerator FadeOutAudioCoroutine()
    {
        float t = 0.0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            // Calculate the new volume of the audio source
            float volume = Mathf.Lerp(initialVolume, 0.0f, t / fadeTime);

            // Set the volume of the audio source
            audioSource.volume = volume;

            yield return null;
        }

        // Stop the audio source
        audioSource.Stop();
    }










    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Increment the timer by the time since last frame

        if (timer >= cycleTime * 60f) // If the timer has reached the cycle time
        {
            isParticleActive = !isParticleActive;
            ToggleParticleState(); // Toggle the state of the particle system
            timer = 0.0f; // Reset the timer
        }
    }

    // Function to toggle the state of the particle system
    void ToggleParticleState()
    {
        if (isParticleActive)
        {
            particle.Stop();
            StopAudioWithFadeOut();
        }
        else
        {
            particle.Play();
            audioSource.volume = initialVolume;
            audioSource.Play();
        }

    }
}
