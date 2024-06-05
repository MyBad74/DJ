using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonHoverEffectNew : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;
    private float originalFontSize;
    private bool isHovered;

    void Start()
    {
        if (buttonText == null)
        {
            buttonText = GetComponentInChildren<TMP_Text>();
        }

        if (buttonText != null)
        {
            originalFontSize = buttonText.fontSize;
        }
        else
        {
            Debug.LogError("TMP_Text component not found in children.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null && !isHovered)
        {
            buttonText.fontSize = originalFontSize * 1.2f;
            buttonText.fontStyle = FontStyles.Bold;
            isHovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.fontSize = originalFontSize;
            buttonText.fontStyle = FontStyles.Normal;
            isHovered = false;
        }
    }

    void Update()
    {
        if (buttonText != null)
        {
            if (isHovered)
            {
                buttonText.transform.localScale = Vector3.Lerp(buttonText.transform.localScale, Vector3.one * 1.2f, Time.deltaTime * 10);
            }
            else
            {
                buttonText.transform.localScale = Vector3.Lerp(buttonText.transform.localScale, Vector3.one, Time.deltaTime * 10);
            }
        }
    }

    // Call this method to reset the button state when the menu is closed
    public void ResetButtonState()
    {
        if (buttonText != null)
        {
            buttonText.fontSize = originalFontSize;
            buttonText.fontStyle = FontStyles.Normal;
            buttonText.transform.localScale = Vector3.one;
        }
        isHovered = false;
    }
}
