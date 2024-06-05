using UnityEngine;

public class LetterButton2 : MonoBehaviour
{
    public Animator santaIsabelAnimator; // Drag the Animator component of SantaIsabel here

    //button to go to scene cards
    public GameObject cardButton;

    //start 
    public void Start()
    {
        cardButton.SetActive(false);
    }

    public void TriggerSantaIsabelAnimation()
    {
        Debug.Log("Triggering Santa Isabel Animation");
        if (santaIsabelAnimator != null)
        {
            santaIsabelAnimator.SetTrigger("Trigger");
        }
        else
        {
            Debug.LogError("Animator not set on " + gameObject.name);
        }
    }

    public void OnAnimationComplete()
    {
        Debug.Log("Animation Complete - Enabling card button");
        cardButton.SetActive(true);
    }
}
