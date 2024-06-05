using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[System.Serializable]
public class Subtitle2
{
    public float startTime; // The time at which to display the subtitle
    public float duration; // How long the subtitle should be displayed
    public string text; // The subtitle text
    public float fontSize; // The font size of the subtitle
    //italic, bold, underline
    public bool italic = false;
}

public class SubManager2 : MonoBehaviour
{
    public TextMeshProUGUI subtitleText; // Reference to the TextMeshProUGUI component
    public List<Subtitle> subtitles; // List of subtitles
    private Coroutine subtitleCoroutine;

    // Method to start showing subtitles
    public void StartSubtitles()
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
        }
        subtitleCoroutine = StartCoroutine(DisplaySubtitles());
    }

    // Method to stop showing subtitles
    public void StopSubtitles()
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
            subtitleText.text = ""; // Clear subtitle text
        }
    }

    IEnumerator DisplaySubtitles()
    {
        // Loop through all the subtitles
        foreach (Subtitle subtitle in subtitles)
        {
            // Wait until the time to display the subtitle
            yield return new WaitForSeconds(subtitle.startTime);

            // Display the subtitle text
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
