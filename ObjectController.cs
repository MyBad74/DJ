// using UnityEngine;
// using System.Collections;

// public class ObjectController : MonoBehaviour
// {
//     public Animator objectAnimator; // Assign in the Inspector
//     public GameObject knight; // Assign in the Inspector
//     public GameObject buttonToAppear; // Drag your button here in the Inspector

//     void Start()
//     {
//         // Initially hide the button.
//         if (buttonToAppear != null)
//         {      
//             buttonToAppear.SetActive(false);
//         }
//     }
    
//     // This function is intended to be called to trigger the slide-down animation.
//     public void TriggerObjectAnimation()
//     {
//         Debug.Log("TriggerObjectAnimation called");
//         objectAnimator.Play("SlideDown"); // Start the sliding animation
//         // The ShowButton and re-enable knight movement will be handled by AnimationComplete via Animation Event
//     }

//     // This method is intended to be called by an Animation Event at the end of the "SlideDown" animation.
//     public void AnimationComplete()
//     {
//         Debug.Log("Animation completed");
//         StartCoroutine(EnableKnightMovementAndShowButton());
//     }

//     // Coroutine to re-enable the knight's movement and show the button, intended to be called after the animation completes.
//     IEnumerator EnableKnightMovementAndShowButton()
//     {
//         // Re-enable knight movement
//         if (knight != null)
//         {
//             KingMovement knightMovement = knight.GetComponent<KingMovement>();
//             if (knightMovement != null)
//             {
//                 knightMovement.enabled = true;
//             }
//         }

//         // Show the button
//         if (buttonToAppear != null)
//         {
//             buttonToAppear.SetActive(true);
//         }

//         yield return null;
//     }
// }
