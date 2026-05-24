using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource ambientSource;

    [Header("Clips")]
    public AudioClip gameBGM;
    public AudioClip clickSFX;
    public AudioClip spinSFX;
    public AudioClip oceanAmbient;
    public AudioClip collectionSFX;
    [Header("Countdown")]
    public AudioClip countdownTick;
    public AudioClip startSFX;

    private void Awake()
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

    // ================= MUSIC =================

    public void PlayGameMusic()
    {
        musicSource.clip = gameBGM;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // ================= CLICK =================

    public void PlayClick()
    {
        sfxSource.PlayOneShot(clickSFX);
    }

    // ================= SPIN X1 =================

    public void PlaySpinOnce()
    {
        sfxSource.PlayOneShot(spinSFX);
    }

    // ================= SPIN LOOP =================

    public void PlaySpinLoop()
    {
        sfxSource.clip = spinSFX;
        sfxSource.loop = true;
        sfxSource.Play();
    }

    public void StopSpinLoop()
    {
        sfxSource.Stop();
        sfxSource.loop = false;
    }
    public IEnumerator FadeOutSpin(float duration)
    {
        float startVolume = sfxSource.volume;

        while (sfxSource.volume > 0)
        {
            sfxSource.volume -= startVolume * Time.deltaTime / duration;

            yield return null;
        }

        sfxSource.Stop();

        sfxSource.volume = startVolume;
        sfxSource.loop = false;
    }
    public void PlayOceanAmbient()
    {
        ambientSource.clip = oceanAmbient;
        ambientSource.loop = true;
        ambientSource.Play();
    }
    public void PauseAmbient()
    {
        ambientSource.Pause();
    }

    public void ResumeAmbient()
    {
        ambientSource.UnPause();
    }
    public void PlayCountdownTick()
    {
        sfxSource.PlayOneShot(countdownTick);
    }

    public void PlayStartSFX()
    {
        sfxSource.PlayOneShot(startSFX);
    }

    public void PLayCollectionSFX()
    {
        sfxSource.PlayOneShot(collectionSFX);
    }    
}