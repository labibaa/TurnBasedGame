using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ScrollPanelController : MonoBehaviour
{
    public GameObject[] panels;
    public ScrollRect[] scrollRects;
    public int visibleButtonCount = 5;
    public float scrollAmount = 100f;

    private Button[][] buttons;
    private int[] selectedIndex;
    private RectTransform[] contentRectTransforms;
    private RectTransform[] viewRectTransforms;
    private float buttonHeight;
    private float[] contentHeights;
    private int activePanelIndex = 0;

    private void Start()
    {
        activePanelIndex = 0;
        int panelCount = panels.Length;
        buttons = new Button[panelCount][];
        selectedIndex = new int[panelCount];
        contentRectTransforms = new RectTransform[panelCount];
        viewRectTransforms = new RectTransform[panelCount];
        contentHeights = new float[panelCount];

        for (int i = 0; i < panelCount; i++)
        {
            buttons[i] = panels[i].GetComponentsInChildren<Button>();
            contentRectTransforms[i] = scrollRects[i].content;
            viewRectTransforms[i] = scrollRects[i].viewport;

            buttonHeight = buttons[i][0].GetComponent<RectTransform>().rect.height;
            contentHeights[i] = buttonHeight * buttons[i].Length;

            // Calculate the height of the visible portion in the panel
            float visibleHeight = buttonHeight * visibleButtonCount;

            // Set the height of the content to fit the visible buttons
            contentRectTransforms[i].sizeDelta = new Vector2(contentRectTransforms[i].sizeDelta.x, visibleHeight);

            // Set the initial selected button
            SelectButton(i, selectedIndex[i]);
        }
    }

    private void OnEnable()
    {
        UiInput.ButtonE += ePressed;
        UiInput.ButtonQ += qPressed;
        UiInput.ButtonW += upArrowPressed;
        UiInput.ButtonS += downArrowPressed;
        UiInput.ButtonEnter += returnPressed;
    }

    private void OnDisable()
    {
        UiInput.ButtonE -= ePressed;
        UiInput.ButtonQ -= qPressed;
        UiInput.ButtonW -= upArrowPressed;
        UiInput.ButtonS -= downArrowPressed;
        UiInput.ButtonEnter -= returnPressed;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        ePressed();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        qPressed();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        upArrowPressed();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.DownArrow))
    //    {
    //        downArrowPressed();
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        returnPressed();
    //    }
    //}

    private void returnPressed()
    {
        PressSelectedButton();
    }

    private void downArrowPressed()
    {
        if (selectedIndex[activePanelIndex] < buttons[activePanelIndex].Length - 1)
        {
            selectedIndex[activePanelIndex]++;
            SelectButton(activePanelIndex, selectedIndex[activePanelIndex]);
        }
    }

    private void upArrowPressed()
    {
        if (selectedIndex[activePanelIndex] > 0)
        {
            selectedIndex[activePanelIndex]--;
            SelectButton(activePanelIndex, selectedIndex[activePanelIndex]);
        }
    }

    private void ePressed()
    {
        activePanelIndex++;
        if (activePanelIndex >= panels.Length)
        {
            activePanelIndex = 0;
            Debug.Log("eeeeeeeeeee000");
        }
            

        SelectButton(activePanelIndex, selectedIndex[activePanelIndex]);
        
    }

    private void qPressed()
    {
        activePanelIndex--;
        if (activePanelIndex < 0)
            activePanelIndex = panels.Length - 1;

        SelectButton(activePanelIndex, selectedIndex[activePanelIndex]);

        Debug.Log("qqqqqqqqqqq");
    }

    private void SelectButton(int panelIndex, int buttonIndex)
    {
        // Reset the scroll position to the top when switching panels
        
            scrollRects[panelIndex].normalizedPosition = new Vector2(0f, 1f);
        

        // Calculate the y position of the selected button
        float buttonY = buttonIndex * buttonHeight;

        // Calculate the offset required to center the selected button
        float offset = Mathf.Clamp(buttonY - (viewRectTransforms[panelIndex].rect.height - buttonHeight) / 2f, 0f, contentHeights[panelIndex] - viewRectTransforms[panelIndex].rect.height);

        // Set the normalized scroll position based on the offset
        float normalizedPosition = offset / (contentHeights[panelIndex] - viewRectTransforms[panelIndex].rect.height);

        // Smoothly scroll to the selected button using LeanTween
        //LeanTween.value(scrollRects[panelIndex].normalizedPosition.y, 1f - normalizedPosition, 0.3f)
        //    .setOnUpdate((float value) =>
        //    {
                
        //        scrollRects[panelIndex].normalizedPosition = new Vector2(scrollRects[panelIndex].normalizedPosition.x, value);
        //    });

        // Scale up the selected button
        for (int i = 0; i < buttons[panelIndex].Length; i++)
        {
            Vector3 scale = buttons[panelIndex][i].transform.localScale;
            scale.y = i == buttonIndex ? 1.2f : 1f;
            buttons[panelIndex][i].transform.localScale = scale;
        }
    }


    private void PressSelectedButton()
    {
        // Retrieve the Button component of the selected button in the active panel
        Button selectedButton = buttons[activePanelIndex][selectedIndex[activePanelIndex]];

        // Check if the button has an onClick event assigned
        if (selectedButton != null && selectedButton.onClick != null)
        {
            // Output a debug message to indicate that the button is pressed
            Debug.Log("Button is pressed!");

            // Execute the onClick event of the selected button
            selectedButton.onClick.Invoke();
        }
    }
}
