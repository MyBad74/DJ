using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Subtitle
{
    public float startTime; // The time at which to display the subtitle
    public float duration; // How long the subtitle should be displayed
    public string text; // The subtitle text
    public float fontSize; // The font size of the subtitle
    //italic, bold, underline
    public bool italic = false;
}


public class SubtitleManager : MonoBehaviour
{
    public TextMeshProUGUI subtitleText; // Reference to the TextMeshProUGUI component
    public List<Subtitle> subtitles; // List of subtitles
    private Coroutine subtitleCoroutine;

    void Start()
    {
        // Start the subtitle coroutine
        subtitleCoroutine = StartCoroutine(DisplaySubtitles());
    }

    IEnumerator DisplaySubtitles()
    {
        // Loop through all the subtitles
        foreach (Subtitle subtitle in subtitles)
        {
            // Wait until the time to display the subtitle
            yield return new WaitForSeconds(subtitle.startTime);

            // Display the subtitle text and set the font size
            subtitleText.text = subtitle.text;
            subtitleText.fontSize = subtitle.fontSize;
            if (subtitle.italic)
            {
                subtitleText.fontStyle = FontStyles.Italic;
            }
            else
            {
                subtitleText.fontStyle = FontStyles.Normal;
            }

            // Wait for the duration of the subtitle
            yield return new WaitForSeconds(subtitle.duration);

            // Clear the subtitle text
            subtitleText.text = "";
        }
    }
}
