using UnityEngine;

public class BirdClick : MonoBehaviour
{
    public GameObject cardWindow; // Reference to the card window
    private bool isClickable = true; // Flag to control clickability

    private void Start()
    {
        cardWindow.SetActive(false); // Ensure the card window is hidden by default
    }

    private void OnMouseDown()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && isClickable)
        {
            // Toggle the visibility of the card window
            cardWindow.SetActive(!cardWindow.activeSelf);
            // Disable further clicks
            isClickable = false;
        }
    }

    // Optionally, you could provide a method to reset the clickability, if needed
    public void ResetClickability()
    {
        isClickable = true;
    }

    void OnDrawGizmosSelected()
    {
        if (GetComponent<BoxCollider2D>() != null)
        {
            Gizmos.color = Color.red;
            Vector3 center = transform.position + new Vector3(GetComponent<BoxCollider2D>().offset.x, GetComponent<BoxCollider2D>().offset.y, 0);
            Vector3 size = new Vector3(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y, 0);
            Gizmos.DrawWireCube(center, size);
        }
    }
}
