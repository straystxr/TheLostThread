using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraneRotation : MonoBehaviour
{
    public float rotationSpeed = 60f;
    private float input;

    public bool canControl = false;

    [Header("Audio")]
    public AudioSource rotationSound;

    [SerializeField] private float fadeOutDuration = 0.2f; // fade out

    private bool isRotating = false; 
    private Coroutine fadeCoroutine; 


    void Update()
    {
        if (!canControl)
        {
            StopRotationSoundWithFade();
            return;
        }
        
        transform.Rotate(Vector3.up, input * rotationSpeed * Time.deltaTime, Space.World);

        if (Mathf.Abs(input) > 0.01f)
        {
            if (!isRotating)
            {
                PlayRotationSound();
            }
        }
        else
        {
            if (isRotating)
            {
                StopRotationSoundWithFade();
            }
        }
    }
      

    public void OnMoveCrane(InputAction.CallbackContext context)
    {
        input = context.ReadValue<float>();
    }

    private void PlayRotationSound()
    {
        if (rotationSound == null) return;

        // Stop any fade that's happening
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        rotationSound.volume = 1f;
        rotationSound.Play();
        isRotating = true;
    }

    private void StopRotationSoundWithFade()
    {
        if (rotationSound == null || !rotationSound.isPlaying) return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutAndStop(rotationSound, fadeOutDuration));
        isRotating = false;
    }

    private IEnumerator FadeOutAndStop(AudioSource audio, float duration)
    {
        float startVolume = audio.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            audio.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        audio.Stop();
        audio.volume = 1f; // reset for next play
        fadeCoroutine = null;
    }
}
