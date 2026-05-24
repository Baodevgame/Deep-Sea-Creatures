using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class MainMenuLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public CanvasGroup menuCanvas;

    IEnumerator Start()
    {
        menuCanvas.alpha = 0;

        // Prepare video
        videoPlayer.Prepare();

        // doi video load frame dau
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
        videoPlayer.Play();
        menuCanvas.alpha = 1;
    }
}