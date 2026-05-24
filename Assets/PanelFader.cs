using System.Collections;
using UnityEngine;

public class PanelFader : MonoBehaviour
{
    public float fadeDuration = 0.25f;

    CanvasGroup cg;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();

        if (cg == null)
        {
            Debug.LogError("Missing CanvasGroup on " + gameObject.name);
        }
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(Fade(0, 1));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(1, 0));
    }

    IEnumerator Fade(float start, float end)
    {
        float time = 0;

        cg.alpha = start;

        if (end == 1)
        {
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;

            cg.alpha = Mathf.Lerp(start, end, time / fadeDuration);

            yield return null;
        }

        cg.alpha = end;

        if (end == 0)
        {
            cg.interactable = false;
            cg.blocksRaycasts = false;

            gameObject.SetActive(false);
        }
    }
}