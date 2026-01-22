using UnityEngine;

public class DebugGlow : MonoBehaviour
{
    public Renderer targetRenderer;
    public Material normalMat;
    public Material glowMat;
    public GameObject interactUI; // NEW

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("PLAYER ENTERED GLOW ZONE");
        targetRenderer.material = glowMat;

        if (interactUI)
            interactUI.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("PLAYER EXITED GLOW ZONE");
        targetRenderer.material = normalMat;

        if (interactUI)
            interactUI.SetActive(false);
    }
}