using UnityEngine;
using System.Collections;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeOutTime = 1.5f;

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        audioSource.volume = 1f;
        audioSource.Play();

        fadeCoroutine = StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < fadeOutTime)
        {
            time += Time.deltaTime;

            // Exponential ease-out (fast drop, long tail)
            float t = time / fadeOutTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t * t);

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}