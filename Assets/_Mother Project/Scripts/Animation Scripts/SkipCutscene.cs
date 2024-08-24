using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class SkipCutscene : MonoBehaviour
{
    public PlayableDirector cutsceneDirector; // Reference to the PlayableDirector controlling the cutscene
    public KeyCode skipKey = KeyCode.E; // The key used to skip the cutscene
    public float holdDuration = 3.0f; // Duration for which the key must be held to skip
    public double skipToTime = 0.0; // The time to skip to in seconds

    public TextMeshProUGUI skipText; // Reference to the TextMeshProUGUI element
    public Image loadingCircle; // Reference to the UI Image for the loading circle

    private float holdTime = 0.0f; // Time the key has been held
    private bool isHolding = false;

    void Update()
    {
        // Check if the skip key is being pressed
        if (Input.GetKey(skipKey))
        {
            holdTime += Time.deltaTime; // Increment hold time
            if (holdTime >= holdDuration)
            {
                SkipCutsceneMethod();
                holdTime = 0.0f; // Reset hold time
                HideSkipUI();
            }
            else
            {
                ShowSkipUI(holdTime / holdDuration);
            }
        }
        else
        {
            holdTime = 0.0f; // Reset hold time if the key is not being pressed
            //HideSkipUI();
            loadingCircle.fillAmount = 0;

        }
    }

    void SkipCutsceneMethod()
    {
        if (cutsceneDirector != null)
        {
            cutsceneDirector.time = skipToTime; // Skip to the specified time frame
            cutsceneDirector.Evaluate(); // Evaluate the cutscene to the new time frame
        }
    }

    void ShowSkipUI(float fillAmount)
    {
        if (!isHolding)
        {
            skipText.gameObject.SetActive(true);
            loadingCircle.gameObject.SetActive(true);
            isHolding = true;
        }
        loadingCircle.fillAmount = fillAmount;
    }

    void HideSkipUI()
    {
        if (isHolding)
        {
            skipText.gameObject.SetActive(false);
            loadingCircle.gameObject.SetActive(false);
            this.enabled = false;
        }
    }
}
