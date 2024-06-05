using UnityEngine;

public class WindowUI : MonoBehaviour
{
    public GameObject cardWindow; // Assign this in the inspector

    private void Start()
    {
        cardWindow.SetActive(false); // Make sure the card window is hidden by default
    }

    public void ToggleCardWindow()
    {
        cardWindow.SetActive(!cardWindow.activeSelf); // Toggle the visibility of the card window
    }
}

