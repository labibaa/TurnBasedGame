using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class ActionNotification : MonoBehaviour
{
  
    public RectTransform notification;
    public TextMeshProUGUI ActionNotificationText;

    public RectTransform notification1;
    public Text ActionNotificationText1;
    public float slideDuration = 1f;
    public float waitDuration = 1f;

 
    public void AnimateNotification(string actionName)
    {
        // Calculate the target position for the slide-in animation
        Vector2 targetPosition = new Vector2(0f, notification.anchoredPosition.y);

        // Slide in from outside the screen on the right

        ActionNotificationText.text = actionName;
        ActionNotificationText.ForceMeshUpdate(true);
        //ActionNotification.
        Debug.Log(actionName);
        notification.anchoredPosition = new Vector2(Screen.width, notification.anchoredPosition.y);
        notification.DOAnchorPosX(targetPosition.x, slideDuration)
            .SetEase(Ease.OutQuint)
            .OnComplete(() => WaitAndSlideOut());

    }

    public void TextEdit(string text)
    {
      
       
        ActionNotificationText1.text = text;
       
        Debug.Log(text);
    }

    private void WaitAndSlideOut()
    {
        // Wait for the specified duration
        DOVirtual.DelayedCall(waitDuration, () =>
        {
            // Calculate the target position for the slide-out animation
            Vector2 targetPosition = new Vector2(Screen.width+300f, notification.anchoredPosition.y);

            // Slide out to the right, outside the screen
            notification.DOAnchorPosX(targetPosition.x, slideDuration)
                .SetEase(Ease.InQuint);
        });
    }
}


