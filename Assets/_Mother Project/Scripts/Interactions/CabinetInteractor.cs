using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CabinetInteractor : MonoBehaviour, IInteractable
{

    public TMP_Text textMeshPro;
    public float textSpeed = 0.01f; // Adjust the typing speed as needed

    private string fullText;
    private string currentText = "";

    public float displayDuration = 5.0f; // Adjust the duration before the text disappears



    private IEnumerator TypeText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];
            textMeshPro.text = currentText;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(displayDuration);
        ClearText();
    }

    public void ShowText(string newText)
    {
        fullText = newText; // Set fullText to the text you want to display
        currentText = ""; // Reset currentText

        StartCoroutine(TypeText());

        Debug.Log("I am TV");
    }
    private void ClearText()
    {
        textMeshPro.text = "";
    }

    public void Interact()
    {
        ShowText("Mon opens the cabinet doors cautiously, revealing a collection of dusty board games and a hint of nostalgia.");

    }

}
