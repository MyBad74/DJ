using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
 
public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;
    public string nextSceneName;

    private bool isFading = false;

    void Start()
    {
        fadeImage.gameObject.SetActive(false);
    }

    public void StartFadeOut()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        isFading = true;
        fadeImage.gameObject.SetActive(true);
        Color fadeColor = fadeImage.color;
        float fadeSpeed = 1f / fadeDuration;

        for (float t = 0; t <= 1; t += Time.deltaTime * fadeSpeed)
        {
            fadeColor.a = t;
            fadeImage.color = fadeColor;
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
