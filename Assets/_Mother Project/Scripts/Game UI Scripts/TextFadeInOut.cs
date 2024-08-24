using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFadeInOut : MonoBehaviour
{
    public static TextFadeInOut instance;
    public TextMeshProUGUI textMeshPro;
    public float fadeInDuration = 1.0f;
    public float fadeOutDuration = 1.0f;

    private Queue<string> textQueue = new Queue<string>();
    private bool isFading = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        // Start with fully transparent text
        textMeshPro.alpha = 0f;
    }

    public void AddTextToQueue(Turn turn)
    {
        // Construct the text entry
        string newText = turn.Player.characterName + " used " + turn.Command.GetActionName();

        // Add the entry to the queue
        textQueue.Enqueue(newText);


        // If not currently fading, start the fade-in process
        //if (!isFading)
        //{
            // Display the next text if the queue is not empty
            if (textQueue.Count > 0)
                DisplayNextText();
        //}
    }

    public void DisplayNextText()
    {
        // Get the next text from the queue
        string nextText = textQueue.Dequeue();

        // Fade in the text
        StartCoroutine(FadeInText(nextText));
    }

    IEnumerator FadeInText(string newText)
    {
        isFading = true;

        // Set the text
        textMeshPro.text += "\n" + newText;

        // Fade in
        float fadeInTime = 0f;
        while (fadeInTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, fadeInTime / fadeInDuration);
            textMeshPro.alpha = alpha;
            fadeInTime += Time.deltaTime;
            yield return null;
        }

        textMeshPro.alpha = 1f;

        isFading = false;
    }

    public void ClearText()
    {
        if (!isFading)
            StartCoroutine(FadeOutAndClearText());
    }

    IEnumerator FadeOutAndClearText()
    {
        isFading = true;

        // Fade out
        float fadeOutTime = 0f;
        while (fadeOutTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, fadeOutTime / fadeOutDuration);
            textMeshPro.alpha = alpha;
            fadeOutTime += Time.deltaTime;
            yield return null;
        }

        textMeshPro.alpha = 0f;

        // Clear the text
        textMeshPro.text = "";

        isFading = false;
    }

    public void RemoveLatestEntry()
    {
        if (!isFading && textMeshPro.text != "")
        {
            // Split text into lines
            string[] lines = textMeshPro.text.Split('\n');

            // Remove the last line
            if (lines.Length > 1)
            {
                string newText = "";
                for (int i = 0; i < lines.Length - 1; i++)
                {
                    if (i > 0)
                        newText += "\n";
                    newText += lines[i];
                }

                // Fade out and update text
                StartCoroutine(FadeOutAndUpdateText(newText));
            }
            else
            {
                // If there's only one line, simply clear the text
                ClearText();
            }
        }
    }

    IEnumerator FadeOutAndUpdateText(string newText)
    {
        isFading = true;

        // Fade out
        float fadeOutTime = 0f;
        while (fadeOutTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, fadeOutTime / fadeOutDuration);
            textMeshPro.alpha = alpha;
            fadeOutTime += Time.deltaTime;
            yield return null;
        }

        // Update text
        textMeshPro.text = newText;

        // Fade in
        float fadeInTime = 0f;
        while (fadeInTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, fadeInTime / fadeInDuration);
            textMeshPro.alpha = alpha;
            fadeInTime += Time.deltaTime;
            yield return null;
        }

        textMeshPro.alpha = 1f;

        isFading = false;
    }
}
