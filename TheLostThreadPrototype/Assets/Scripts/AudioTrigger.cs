using UnityEngine;
using System.Collections;

public class AudioTrigger : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource playerSplash;  
    public AudioSource playerPain;   
    public AudioSource metalSplash;
    public AudioSource metalMagnet; // New field

    [Header("Fade Settings")]
    public float fadeOutTime = 1.5f; 

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play playerSplash with fade
            if (playerSplash != null)
            {
                if (fadeCoroutine != null)
                    StopCoroutine(fadeCoroutine);

                playerSplash.volume = 1f;
                playerSplash.Play();
                fadeCoroutine = StartCoroutine(FadeOutPlayerSplash());
            }

            
            if (playerPain != null)
                playerPain.Play();
        }
        else if (other.CompareTag("Metal"))
        {
            
            if (metalSplash != null)
                metalSplash.Play();

            
            if (metalMagnet != null)
                metalMagnet.Play();
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