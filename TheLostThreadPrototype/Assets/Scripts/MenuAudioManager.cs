using UnityEngine;
using System.Collections;

public class MenuAudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource flickerSource;

    [Header("Clips")]
    public AudioClip backgroundMusic;
    public AudioClip flickerSound;

    [Header("Settings")]
    public float flickerIntervalMin = 3f;
    public float flickerIntervalMax = 7f;
    public float fadeOutTime = 2f;

    public static MenuAudioManager Instance; 
    //static - a singleton of that script - meaning there's only a single 
    //of this class

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Start background music
        if (bgmSource && backgroundMusic)
        {
            bgmSource.clip = backgroundMusic;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        // Start flicker coroutine
        if (flickerSource && flickerSound)
            StartCoroutine(PlayFlickerRandomly());
    }

    IEnumerator PlayFlickerRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(flickerIntervalMin, flickerIntervalMax));
            flickerSource.PlayOneShot(flickerSound);
        }
    }

    public void FadeOutMusicAndStop()
    {
        StartCoroutine(FadeOut(bgmSource, fadeOutTime));
    }

    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}