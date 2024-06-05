// using UnityEngine;
// using UnityEngine.UI; // Required for UI elements like Button

// public class GameManager : MonoBehaviour
// {
//     public GameObject knight; // Assign your knight GameObject in the Inspector
//     public GameObject respawnUI; // Assign your panel or button that contains the respawn text

//     private void Start()
//     {
//         respawnUI.SetActive(false); // Hide the respawn UI at the start
//     }

//     // Call this from the KnightHealth script when the knight dies
//     public void ShowRespawnUI()
//     {
//         respawnUI.SetActive(true); // Show the respawn UI
//     }

//     // Assign this to the Respawn button's OnClick() event in the Inspector
//     public void RespawnKnight()
//     {
//         knight.SetActive(true); // Reactivate the knight if it was deactivated
//         KnightHealth knightHealth = knight.GetComponent<KnightHealth>();
//         if (knightHealth != null)
//         {
//             knightHealth.ResetHealth(); // Reset the knight's health
//             knightHealth.Respawn(); // Add a method to handle additional respawn logic
//         }
//         // Hide the respawn UI if it's a separate UI element
//         respawnUI.SetActive(false);
//     }

// }
