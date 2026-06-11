using UnityEngine;

public static class AdsAudioUtility
{
    public static void Pause()
    {
        if (AudioManager.Instance == null)
            return;

        AudioManager.Instance.musicSource?.Pause();
        AudioManager.Instance.ambientSource?.Pause();
    }

    public static void Resume()
    {
        if (AudioManager.Instance == null)
            return;

        AudioManager.Instance.musicSource?.UnPause();
        AudioManager.Instance.ambientSource?.UnPause();
    }
}