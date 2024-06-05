using UnityEngine;
using UnityEngine.EventSystems; // Required for event handlers
using UnityEngine.UI; // Required for UI elements like Image

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage;
    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray; // Set your hover color here

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.color = normalColor; // Set initial color
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = hoverColor; // Change to hover color when mouse enters
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = normalColor; // Revert to normal color when mouse exits
    }
}
