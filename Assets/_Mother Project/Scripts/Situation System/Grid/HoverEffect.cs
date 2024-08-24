using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    public Color hoverColor = Color.blue; // Color when hovered
    private Color originalColor; // Original color of the GameObject
    private bool isHovered = false; // Flag to track whether the mouse is currently over the GameObject

    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    void OnMouseEnter()
    {
        if (!isHovered)
        {
            isHovered = true;
            GetComponent<Renderer>().material.color = hoverColor;
            Debug.Log("this"+gameObject.name);
        }
    }

    void OnMouseExit()
    {
        if (isHovered)
        {
            isHovered = false;
            GetComponent<Renderer>().material.color = originalColor;
            Debug.Log("this out" + gameObject.name);
        }
    }
}
