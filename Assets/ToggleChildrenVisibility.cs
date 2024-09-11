using UnityEngine;
using UnityEngine.UI;

public class ToggleChildrenVisibility : MonoBehaviour
{
    [SerializeField] private GameObject parentObject; // Parent GameObject containing the child images
    private bool areChildrenVisible = false; // Start with children being invisible
    private Button button; // The button this script is attached to
    private CanvasGroup canvasGroup; // CanvasGroup to control visibility and interactivity

    void Start()
    {
        // Get the Button and CanvasGroup components
        button = GetComponent<Button>();
        canvasGroup = GetComponent<CanvasGroup>();

        // If CanvasGroup is not already attached, add one
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Ensure children are hidden at the start
        SetChildrenVisibility(false);

        // Check and set button visibility based on GridSystem
        CheckButtonVisibility();
    }

    // Toggle visibility when the button is pressed
    public void ToggleVisibility()
    {
        areChildrenVisible = !areChildrenVisible;
        SetChildrenVisibility(areChildrenVisible);
    }

    // Set visibility for all child images
    private void SetChildrenVisibility(bool isVisible)
    {
        Image[] childImages = parentObject.GetComponentsInChildren<Image>(true);
        foreach (Image img in childImages)
        {
            img.gameObject.SetActive(isVisible);
        }
    }

    // Continuously check the grid state and update button visibility
    void Update()
    {
        if (!areChildrenVisible)
        {
            // Continuously check and hide any newly instantiated child images
            SetChildrenVisibility(false);
        }
        CheckButtonVisibility(); // Constantly check button visibility based on grid state

    }

    // Check if the button should be visible and interactable based on GridSystem
    private void CheckButtonVisibility()
    {
        if (GridSystem.instance != null && button != null)
        {
            bool isGridOn = GridSystem.instance.IsGridOn; // Cache the grid state
            

            // Set the button's visibility and interactability based on grid state
            if (isGridOn)
            {
                canvasGroup.alpha = 1f;  // Fully visible
                canvasGroup.interactable = true;  // Interactable
                canvasGroup.blocksRaycasts = true; // Allows clicking
            }
            else
            {
                canvasGroup.alpha = 0f;  // Fully invisible
                canvasGroup.interactable = false;  // Non-interactable
                canvasGroup.blocksRaycasts = false; // Prevents clicking
            }
        }
        else
        {
            Debug.LogWarning("GridSystem instance or button is null!");
        }
    }
}
