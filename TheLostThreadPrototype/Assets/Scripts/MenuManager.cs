using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("Transition Settings")]
    public Animator cameraAnimator; // Animator on the camera
    public string cameraAnimName = "CameraZoom";
    public Image fadeImage;
    public float fadeSpeed = 1.5f;
    public string nextSceneName = "GameScene";
    
    [Header("Music")]
   // public MenuMusicManager menuMusicManager;

    private bool isTransitioning = false;
    
    public void PlayGame()
    {
        if (isTransitioning) return;
        isTransitioning = true;

        /* bringing up errors, removing to reduce issues - nir
        // 1️⃣ Fade music
        if (menuMusicManager != null)
            menuMusicManager.FadeOutMusic();

        // 2️⃣ Start camera/UI fade and scene load
        StartCoroutine(PlayTransition());*/
    }

    private IEnumerator PlayTransition()
    {
        // Play camera animation
        if (cameraAnimator != null)
            cameraAnimator.Play(cameraAnimName);

        // Wait for animation length
        float animLength = cameraAnimator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength);

        // Fade to black
        Color color = fadeImage.color;
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        // Load the next scene AFTER fade + camera animation
        SceneManager.LoadScene(nextSceneName);
    }
}
