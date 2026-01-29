using UnityEngine;

public class StickyNoteInteractable : MonoBehaviour, IInteractable
{
    [Header("Sticky Note Data")]
    public int noteNumber;
    public Color noteColor;

    [Header("UI")]
    public GameObject noteCanvas; // Assign your existing canvas here

    private bool collected = false;

    public bool CanHold => false;

    public void Interact(Transform interactor)
    {
        if (collected) return;

        collected = true;

        // Show UI sticky note
        if (noteCanvas != null)
            noteCanvas.SetActive(true);

        // Optional: sound / VFX here

        // Remove from world
        gameObject.SetActive(false);
    }

    public void Release()
    {
        // Not needed for sticky notes
    }
}