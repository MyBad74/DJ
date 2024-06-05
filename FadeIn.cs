using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public Image fadeImage;  // Assign in Inspector
    public float fadeDuration = 2f;

    private void Start()
    {
        if (fadeImage != null)
        {
            StartCoroutine(FadeInn());
        }
    }

    private IEnumerator FadeInn()
    {
        float elapsed = 0f;
        Color fadeColor = fadeImage.color;
        fadeColor.a = 1f;  // Start fully opaque
        fadeImage.color = fadeColor;

        while (elapsed < fadeDuration)
        {
            fadeColor.a = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = fadeColor;
            elapsed += Time.deltaTime;
            yield return null;
        }

        fadeColor.a = 0f;  // End fully transparent
        fadeImage.color = fadeColor;
        fadeImage.gameObject.SetActive(false);  // Optionally disable the image
    }
}
