using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void StopSoundEffect()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
