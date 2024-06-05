using UnityEngine;

public class BirdManager : MonoBehaviour
{
    private Animator animator;
    public AudioSource walkingSound; // Already existing AudioSource for footsteps
    public AudioSource birdSound;    // Add this second AudioSource for the bird sound through Inspector

    void Start()
    {
        animator = GetComponent<Animator>();
        // Ensure both AudioSources are assigned through the inspector
    }

    public void TriggerBirdAction()
    {
        animator.SetTrigger("Trigger");
    }

    // Call this method using an Animation Event at the point where you want the sound to play
    public void PlayBirdSound()
    {
        if (birdSound != null && !birdSound.isPlaying)
        {
            Debug.Log("Bird sound played");
            birdSound.Play();
        }
    }
}
