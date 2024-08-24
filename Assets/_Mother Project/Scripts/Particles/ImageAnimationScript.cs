using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageAnimationScript : MonoBehaviour
{
    public Image image; // Reference to your image
    private Vector3 initialPosition = new Vector3(1000f, 0f, 0f); // Initial position outside the right side of the screen

    public void StartImageAnimation()
    {
        image.rectTransform.anchoredPosition = initialPosition; // Set the initial position of the image
        image.gameObject.SetActive(true); // Ensure the image is active

        // Use DOTween to animate the image's position from the initial position to the center of the screen
        image.rectTransform.DOAnchorPos(Vector2.zero, 1f)
            .OnComplete(() =>
            {
                // Animation complete, wait for 5 seconds before animating the image back to its initial position
                DOVirtual.DelayedCall(5f, () =>
                {
                    // Use DOTween to animate the image's position back to its initial position
                    image.rectTransform.DOAnchorPos(initialPosition, 1f)
                        .OnComplete(() =>
                        {
                            // Animation complete, hide the image
                            image.gameObject.SetActive(false);
                        })
                        .SetEase(Ease.OutCubic); // You can adjust the ease type if desired
                });
            })
            .SetEase(Ease.OutCubic); // You can adjust the ease type if desired
    }





    private void Start()
    {
        StartImageAnimation();
    }
}
