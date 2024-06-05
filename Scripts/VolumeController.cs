using UnityEngine;
using UnityEngine.UI; // Required for interacting with UI elements

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the UI slider
    public Slider musicSlider; // Reference to the UI slider
    public AudioSource footstepSource; // Reference to the AudioSource that plays footsteps
    public AudioSource backgroundMusic; // Reference to the AudioSource that plays background music

    void Start()
    {
        // Initialize the slider value to the current volume
        if (footstepSource != null)
            volumeSlider.value = footstepSource.volume;

        // Initialize the music slider value to the current volume
        if (backgroundMusic != null)
            musicSlider.value = backgroundMusic.volume;

        // Add a listener to handle volume change
        musicSlider.onValueChanged.AddListener(HandleMusicChange);

        // Add a listener to handle volume change
        volumeSlider.onValueChanged.AddListener(HandleVolumeChange);
    }

    void HandleVolumeChange(float volume)
    {
        if (footstepSource != null)
            footstepSource.volume = volume;
    }

    void HandleMusicChange(float volume)
    {
        if (backgroundMusic != null)
            backgroundMusic.volume = volume;
    }
}
