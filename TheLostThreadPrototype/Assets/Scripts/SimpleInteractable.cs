using UnityEngine;

public class SimpleInteractable : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Material glowMaterial;
    [SerializeField] private GameObject interactUI;

    private Material[] originalMaterials;

    void Awake()
    {
        originalMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
            originalMaterials[i] = renderers[i].material;

        if (interactUI)
            interactUI.SetActive(false);
    }

    public void ShowFeedback()
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material = glowMaterial;

        if (interactUI)
            interactUI.SetActive(true);
    }

    public void HideFeedback()
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material = originalMaterials[i];

        if (interactUI)
            interactUI.SetActive(false);
    }
}