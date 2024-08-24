using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTimer : MonoBehaviour
{
    public static TurnTimer Instance { get; private set; }

    private Coroutine timerCoroutine;
    [SerializeField]
    float durationInSeconds;
    private void Awake()
    {
        // Check if an instance of TurnTimer already exists
        if (Instance == null)
        {
            // If not, set the instance to this one and make it persistent
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the instance alive across scenes
        }
        else if (Instance != this)
        {
            // If an instance already exists and it's not this one, destroy this object
            Destroy(gameObject);
        }
    }
    public void StartTimer()
    {
        // Start the coroutine to handle the timing
       timerCoroutine= StartCoroutine(TimerCoroutine(durationInSeconds));
    }
    public void StopTImer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
    }
    // Coroutine to wait for the specified time and then call endTurn
    private IEnumerator TimerCoroutine(float durationInSecondsLocal)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(durationInSecondsLocal);

        // Call the endTurn function after the time has elapsed
        TurnManager.instance.EndTurn();
    }
}
