using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSoundManager : MonoBehaviour
{
    public AudioClip[] sceneAudioClips;
    public float[] sceneAudioVolumes;

    void Start()
    {
        // Update the sounds in Smanager when the scene starts
        if (Smanager.Instance != null)
        {
            Smanager.Instance.UpdateSounds(sceneAudioClips, sceneAudioVolumes);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update the sounds in Smanager when the scene changes
        if (Smanager.Instance != null)
        {
            Smanager.Instance.UpdateSounds(sceneAudioClips, sceneAudioVolumes);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
