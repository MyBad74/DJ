using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
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
            StartCoroutine(FadeOutAndStop());
        }
    }

    private IEnumerator FadeOutAndStop(float fadeDuration = 1f)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset volume to original level
    }
}
