using UnityEngine;

public class Smanager : MonoBehaviour
{
    public static Smanager Instance { get; private set; }

    public AudioClip[] audioClips;
    public float[] audioVolumes; // Array to hold volume levels for each clip
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton pattern to ensure only one instance of Smanager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on the GameObject. Please add an AudioSource component.");
        }
    }

    // Method to update audio clips and volumes
    public void UpdateSounds(AudioClip[] newClips, float[] newVolumes)
    {
        if (newClips.Length != newVolumes.Length)
        {
            Debug.LogError("Audio volumes array must be the same length as audio clips array.");
            return;
        }

        audioClips = newClips;
        audioVolumes = newVolumes;
    }

    public void PlaySound(int clipIndex)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("Cannot play sound. AudioSource is missing.");
            return;
        }

        if (clipIndex >= 0 && clipIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[clipIndex];
            audioSource.volume = audioVolumes[clipIndex]; // Set the volume for the clip
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Clip index out of range!");
        }
    }

    public void StopSound(int clipIndex)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("Cannot stop sound. AudioSource is missing.");
            return;
        }

        if (clipIndex >= 0 && clipIndex < audioClips.Length)
        {
            audioSource.Stop();
        }
        else
        {
            Debug.LogWarning("Clip index out of range!");
        }
    }
}
