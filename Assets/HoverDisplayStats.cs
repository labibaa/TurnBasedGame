using UnityEngine;
using UnityEngine.EventSystems;  // Needed for the IPointerEnterHandler and IPointerExitHandler
using TMPro;  // TextMesh Pro namespace

public class HoverDisplayStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CharacterBaseClasses myCharacter;  // Assign your character in the inspector or dynamically
    public TextMeshProUGUI statText;  // Assign the TextMesh Pro text component in the inspector

    private TemporaryStats characterStats;  // To store the stats component

    private void Start()
    {
        // Get the TemporaryStats component from the character
        if (myCharacter != null)
        {
            characterStats = myCharacter.GetComponent<TemporaryStats>();
        }
    }

    // Called when the mouse pointer enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (characterStats != null)
        {
            // Update the TextMesh Pro text with the character's HP and AP
            statText.text = "HP: " + characterStats.CurrentHealth.ToString() + "\nAP: " + characterStats.CurrentAP.ToString();
            statText.gameObject.SetActive(true);  // Show the text when hovering
        }
    }

    // Called when the mouse pointer exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        statText.gameObject.SetActive(false);  // Hide the text when not hovering
    }
}
