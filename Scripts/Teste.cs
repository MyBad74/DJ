using UnityEngine;
using UnityEngine.EventSystems; // Required for handling pointer events

public class Teste : MonoBehaviour, IPointerClickHandler
{
    private int clickCount = 0; // Variable to keep track of the number of clicks

    public void OnPointerClick(PointerEventData eventData)
    {
        clickCount++; // Increment the click count by 1 each time the image is clicked
        Debug.Log($"Image clicked {clickCount} times."); // Output the click count to the Unity Console
    }
}
