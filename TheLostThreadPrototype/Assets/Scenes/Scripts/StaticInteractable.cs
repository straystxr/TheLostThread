using UnityEngine;

public class StaticInteractable : MonoBehaviour, IInteractable
{
    [Header("Feedback")]
    public GameObject interactUI;
    public Behaviour outline; // outline or glow component

    protected virtual void Start()
    {
        if (interactUI) interactUI.SetActive(false);
        if (outline) outline.enabled = false;
    }

    public virtual void OnFocus()
    {
        if (interactUI) interactUI.SetActive(true);
        if (outline) outline.enabled = true;
    }

    public virtual void OnUnfocus()
    {
        if (interactUI) interactUI.SetActive(false);
        if (outline) outline.enabled = false;
    }

    public virtual void Interact(Transform interactor) { }
    public virtual void Release() { }
}