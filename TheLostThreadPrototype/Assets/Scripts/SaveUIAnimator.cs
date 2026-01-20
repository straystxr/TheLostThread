using UnityEngine;
using System.Collections;

public class SaveUIAnimator : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Animator animator;
    public string animationStateName = "AutoSave";
    public float showTime = 1f;

    private Coroutine routine;

    private void Awake()
    {
        if (canvasGroup != null)
            canvasGroup.alpha = 0f; // invisible by default
    }

    public void PlaySaveAnimation()
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        if (canvasGroup != null)
            canvasGroup.alpha = 1f; // make visible

        if (animator != null)
            animator.Play(animationStateName, 0, 0f); // play animation

        yield return new WaitForSeconds(showTime); // wait for animation duration

        if (canvasGroup != null)
            canvasGroup.alpha = 0f; // hide again
    }
}