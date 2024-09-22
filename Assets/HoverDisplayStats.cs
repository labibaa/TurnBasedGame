using UnityEngine;
using UnityEngine.EventSystems;  // Needed for IPointerEnterHandler and IPointerExitHandler
using TMPro;  // TextMesh Pro namespace

public class HoverDisplayStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject myCharacter;  // Assign your character in the inspector
    public TextMeshProUGUI hpText;  // Assign the TextMesh Pro text component for HP
    public TextMeshProUGUI apText;  // Assign the TextMesh Pro text component for AP

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
            // Update the HP and AP TextMesh Pro components separately
            hpText.text = characterStats.CurrentHealth.ToString();
            apText.text = characterStats.CurrentAP.ToString();

            // Make sure both text objects are visible
            hpText.gameObject.SetActive(true);
            apText.gameObject.SetActive(true);
        }
    }

    // Called when the mouse pointer exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the HP and AP text when not hovering
        hpText.gameObject.SetActive(false);
        apText.gameObject.SetActive(false);
    }
}
