using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;

    public Image fadeImage;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        fadeImage.color = new Color(0,0,0,0); // transparent
    }

    public IEnumerator FadeOut(float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0,0,0, Mathf.Clamp01(t/duration));
            yield return null;
        }
    }

    public IEnumerator FadeIn(float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0,0,0, Mathf.Clamp01(1 - t/duration));
            yield return null;
        }
    }
}