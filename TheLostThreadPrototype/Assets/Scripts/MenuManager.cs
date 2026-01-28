using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("Transition Settings")]
    public Animator cameraAnimator;   // Animator on the camera
    public string cameraAnimName = "CameraZoom";
    public Image fadeImage;
    public float fadeSpeed = 1.5f;
    public string nextSceneName = "GameScene";

    private bool isTransitioning = false;

    public void PlayGame()
    {
        if (!isTransitioning)
            StartCoroutine(PlayTransition());
    }

    private IEnumerator PlayTransition()
    {
        isTransitioning = true;

        // Play camera animation
        cameraAnimator.Play(cameraAnimName);

        // Wait for the animation to finish
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

        //Load next scene
        SceneManager.LoadScene(nextSceneName);
    }
}