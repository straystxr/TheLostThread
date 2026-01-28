using UnityEngine;

public class SimpleMonitorFlicker : MonoBehaviour
{
    public Renderer screenRenderer;
    public Color flickerColor = Color.cyan;
    public float flickerSpeed = 0.1f; // seconds between flickers
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;

    private Material mat;
    private float timer;

    void Start()
    {
        mat = screenRenderer.material;
        mat.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= flickerSpeed)
        {
            float intensity = Random.Range(minIntensity, maxIntensity);
            Color finalColor = flickerColor * Mathf.LinearToGammaSpace(intensity);
            mat.SetColor("_EmissionColor", finalColor);
            timer = 0f;
        }
    }
}