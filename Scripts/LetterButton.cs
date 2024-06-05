using UnityEngine;

public class LetterButton : MonoBehaviour
{
    public Animator santaIsabelAnimator; // Drag the Animator component of SantaIsabel here

    //window of the letter
    public GameObject letterWindow;

    //button to go to scene cards
    public GameObject cardButton;

    //images of the light cmoing with santa isabel
    public GameObject lightImage;

    //start 
    public void Start()
    {
        cardButton.SetActive(false);
        lightImage.SetActive(false);
    }

    public void TriggerSantaIsabelAnimation()
    {
        Debug.Log("Triggering Santa Isabel Animation");
        if (santaIsabelAnimator != null)
        {
            santaIsabelAnimator.SetTrigger("Trigger");
            lightImage.SetActive(true);
            letterWindow.SetActive(false);
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
