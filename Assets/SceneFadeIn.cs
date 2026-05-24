using UnityEngine;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 0.8f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0f;

        fadeCanvas.alpha = 1f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            fadeCanvas.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);

            yield return null;
        }

        fadeCanvas.alpha = 0f;

        gameObject.SetActive(false);
    }
}