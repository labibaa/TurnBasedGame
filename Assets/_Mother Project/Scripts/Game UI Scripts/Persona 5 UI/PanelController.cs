using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelController : MonoBehaviour
{
    public RectTransform[] panels;
    public float selectedPanelScale = 1.2f;
    public float deselectedPanelScale = 0.8f;
    public float panelMoveDistance = 20f;
    public float panelMoveDuration = 0.2f;

    private int currentPanelIndex = 0;

    private void OnEnable()
    {
        UiInput.ButtonE += ePressed;
        UiInput.ButtonQ += qPressed;
        //UiInput.ButtonW += upArrowPressed;
        //UiInput.ButtonS += downArrowPressed;
        //UiInput.ButtonEnter += returnPressed;
    }

    private void OnDisable()
    {
        UiInput.ButtonE -= ePressed;
        UiInput.ButtonQ -= qPressed;
        //UiInput.ButtonW -= upArrowPressed;
        //UiInput.ButtonS -= downArrowPressed;
        //UiInput.ButtonEnter -= returnPressed;
    }
    private void Start()
    {
        InitializePanels();
    }

   

    private void ePressed()
    {
        SwitchToNextPanel();
        Debug.Log(" e press");
    }

    private void qPressed()
    {
        SwitchToPreviousPanel();
        Debug.Log("q PRESS");
    }

    private void InitializePanels()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            RectTransform panel = panels[i];
            panel.localScale = Vector3.one * deselectedPanelScale;
            panel.SetAsFirstSibling();

            if (i == currentPanelIndex)
            {
                panel.localScale = Vector3.one * selectedPanelScale;
                EnableScrollRect(panel, true);
            }
            else
            {
                float targetPositionX = (i < currentPanelIndex) ? -panelMoveDistance : panelMoveDistance;
                LeanTween.moveLocalX(panel.gameObject, targetPositionX, 0f);
                EnableScrollRect(panel, false);
            }
        }
    }

    private void SwitchToPreviousPanel()
    {
        int previousPanelIndex = (currentPanelIndex - 1 + panels.Length) % panels.Length;

        RectTransform previousPanel = panels[previousPanelIndex];
        RectTransform selectedPanel = panels[currentPanelIndex];

        //LeanTween.scale(selectedPanel.gameObject, Vector3.one * deselectedPanelScale, panelMoveDuration)
        //    .setEase(LeanTweenType.easeOutBack);

        //LeanTween.scale(previousPanel.gameObject, Vector3.one * selectedPanelScale, panelMoveDuration)
        //    .setEase(LeanTweenType.easeOutBack);

        selectedPanel.DOScale(Vector3.one * deselectedPanelScale, panelMoveDuration)
          .SetEase(Ease.OutBack);

        previousPanel.DOScale(Vector3.one * selectedPanelScale, panelMoveDuration)
            .SetEase(Ease.OutBack);


        //selectedPanel.gameObject.transform.position = selectedPanel.gameObject.transform.position + new Vector3(10f,0f,0f) ;

        selectedPanel.DOLocalMoveX(selectedPanel.localPosition.x + panelMoveDistance, panelMoveDuration)
           .SetEase(Ease.OutBack);

        // previousPanel.gameObject.transform.position = previousPanel.gameObject.transform.position - new Vector3(10f,0f,0f);

        previousPanel.DOLocalMoveX(0f, panelMoveDuration)
            .SetEase(Ease.OutBack);


        //LeanTween.moveLocalX(selectedPanel.gameObject, panelMoveDistance, panelMoveDuration)
        //    .setEase(LeanTweenType.easeOutBack);

        //LeanTween.moveLocalX(previousPanel.gameObject, 0f, panelMoveDuration)
        //    .setEase(LeanTweenType.easeOutBack);

        EnableScrollRect(selectedPanel, false);
        EnableScrollRect(previousPanel, true);

        previousPanel.SetAsLastSibling(); // Move the previous panel to the bottom

        currentPanelIndex = previousPanelIndex;
    }

    private void SwitchToNextPanel()
    {
        
        int nextPanelIndex = (currentPanelIndex + 1) % panels.Length;

        RectTransform nextPanel = panels[nextPanelIndex];
        RectTransform selectedPanel = panels[currentPanelIndex];

        //LeanTween.scale(selectedPanel.gameObject, Vector3.one * deselectedPanelScale, panelMoveDuration)
        //    .setEase(LeanTweenType.easeOutBack);

        //LeanTween.scale(nextPanel.gameObject, Vector3.one * selectedPanelScale, panelMoveDuration)
        //    .setEase(LeanTweenType.easeOutBack);

        //LeanTween.moveLocalX(selectedPanel.gameObject, -panelMoveDistance, panelMoveDuration)
        //    .setEase(LeanTweenType.easeOutBack);

        //LeanTween.moveLocalX(nextPanel.gameObject, 0f, panelMoveDuration)
        //    .setEase(LeanTweenType.easeOutBack);


        selectedPanel.DOScale(Vector3.one * deselectedPanelScale, panelMoveDuration)
            .SetEase(Ease.OutBack);

        nextPanel.DOScale(Vector3.one * selectedPanelScale, panelMoveDuration)
            .SetEase(Ease.OutBack);

        selectedPanel.DOLocalMoveX(selectedPanel.localPosition.x - panelMoveDistance, panelMoveDuration)
            .SetEase(Ease.OutBack);

        nextPanel.DOLocalMoveX(0f, panelMoveDuration)
            .SetEase(Ease.OutBack);

        EnableScrollRect(selectedPanel, false);
        EnableScrollRect(nextPanel, true);

        nextPanel.SetAsLastSibling(); // Move the next panel to the bottom

        currentPanelIndex = nextPanelIndex;
    }



    private void EnableScrollRect(RectTransform panel, bool enable)
    {
        ScrollRect scrollRect = panel.GetComponent<ScrollRect>();
        if (scrollRect != null)
        {
            scrollRect.enabled = enable;
        }
    }
}