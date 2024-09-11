using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f); // Scale when hovered
    private Vector3 originalScale;
    public float tweenDuration = 0.2f; // Duration of the tween effect

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Smoothly scale to the hover scale
        LeanTween.scale(gameObject, hoverScale, tweenDuration).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Smoothly scale back to the original scale
        LeanTween.scale(gameObject, originalScale, tweenDuration).setEase(LeanTweenType.easeOutQuad);
    }
}
