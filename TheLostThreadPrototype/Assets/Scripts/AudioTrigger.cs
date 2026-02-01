using UnityEngine;
using System.Collections;

public class AudioTrigger : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource playerSplash;  // First player sound (fading)
    public AudioSource playerPain;    // Second player sound (plays instantly)
    public AudioSource metalSplash;   // Metal sound

    [Header("Fade Settings")]
    public float fadeOutTime = 1.5f;

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop previous fade if still running
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            // Stop metal splash if playing
            metalSplash.Stop();

            // Play player splash with fade
            playerSplash.volume = 1f;
            playerSplash.Play();
            fadeCoroutine = StartCoroutine(FadeOutPlayerSplash());

            // Play player pain instantly
            playerPain.volume = 1f;
            playerPain.Play();
        }
        else if (other.CompareTag("Metal"))
        {
            // Stop any player sounds
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
                fadeCoroutine = null;
            }

            playerSplash.Stop();
            playerPain.Stop();

            // Play metal splash
            metalSplash.volume = 1f;
            metalSplash.Play();
        }
    }

    private IEnumerator FadeOutPlayerSplash()
    {
        float startVolume = playerSplash.volume;
        float time = 0f;

        while (time < fadeOutTime)
        {
            time += Time.deltaTime;
            float t = time / fadeOutTime;
            playerSplash.volume = Mathf.Lerp(startVolume, 0f, t * t);
            yield return null;
        }

        playerSplash.Stop();
        playerSplash.volume = startVolume;
    }
}