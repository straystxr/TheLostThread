using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuTransition : MonoBehaviour
{
    [Header("References")]
    public Animator cameraAnimator;     // Animator on the Main Camera
    public Image fadeImage;             // Fullscreen black image (alpha 0 at start)
    public CanvasGroup uiGroup;         // CanvasGroup for the title/buttons

    [Header("Settings")]
    public string nextSceneName = "SampleScene";
    public float uiFadeDuration = 1f;
    public float cameraAnimDuration = 2.5f; // length of your camera zoom animation
    public float fadeToBlackDuration = 1.2f;

    private bool isTransitioning = false;

    // 🎮 Called by the Play button
    public void OnPlayPressed()
    {
        if (isTransitioning) return;
        isTransitioning = true;

        // Fade out background music 
        if (MenuAudioManager.Instance != null)
            MenuAudioManager.Instance.FadeOutMusicAndStop();

        // Start the transition sequence
        StartCoroutine(TransitionSequence());
    }

    private IEnumerator TransitionSequence()
    {
        // Fade out the UI
        float elapsed = 0f;
        while (elapsed < uiFadeDuration)
        {
            elapsed += Time.deltaTime;
            uiGroup.alpha = 1 - (elapsed / uiFadeDuration);
            yield return null;
        }
        uiGroup.alpha = 0;
        uiGroup.interactable = false;
        uiGroup.blocksRaycasts = false;

        // Play the camera animation
        if (cameraAnimator != null)
            cameraAnimator.SetTrigger("PlayZoom");

        yield return new WaitForSeconds(cameraAnimDuration);

        //Fade to black
        float alpha = 0f;
        Color c = fadeImage.color;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeToBlackDuration;
            c.a = alpha;
            fadeImage.color = c;
            yield return null;
        }

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
