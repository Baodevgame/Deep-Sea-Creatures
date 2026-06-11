using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFader : MonoBehaviour
{
    public static CameraFader Instance;

    [Header("Fade Settings")]
    public float duration;

    float alpha = 0f;
    bool isFading = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeSwitchCanvas(
        GameObject current,
        GameObject target)
    {
        if (isFading) return;

        StartCoroutine(FadeCanvasRoutine(current, target));
    }

    IEnumerator FadeCanvasRoutine(
        GameObject current,
        GameObject target)
    {
        isFading = true;

        yield return Fade(0f, 1f);

        if (current != null)current.SetActive(false);

        if (target != null)target.SetActive(true);

        yield return Fade(1f, 0f);

        isFading = false;
    }

    IEnumerator Fade(float from, float to)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;

            alpha =Mathf.Lerp(from, to, t / duration);

            yield return null;
        }

        alpha = to;
    }

    void OnGUI()
    {
        if (alpha <= 0f) return;

        Color c = Color.black;
        c.a = alpha;
        GUI.color = c;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),Texture2D.whiteTexture);
    }
    public void FadeToScene(string sceneName)
    {
        if (isFading) return;

        StartCoroutine(FadeSceneRoutine(sceneName));
    }

    IEnumerator FadeSceneRoutine(string sceneName)
    {
        isFading = true;

        yield return Fade(0f, 1f);

        SceneManager.LoadScene(sceneName);

        yield return null;

        yield return Fade(1f, 0f);

        isFading = false;
    }
    public Coroutine FadeIn()
    {
        return StartCoroutine(Fade(1f, 0f));
    }

    public Coroutine FadeOut()
    {
        return StartCoroutine(Fade(0f, 1f));
    }
}