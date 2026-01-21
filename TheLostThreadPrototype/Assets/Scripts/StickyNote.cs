using UnityEngine;

public class StickyNote : MonoBehaviour
{
    public int noteNumber;
    public Color noteColor;

    private bool collected = false;

    public void Collect()
    {
        if (collected) return;
        collected = true;

        // Inform UI
        StickyNoteUIManager.Instance.AddStickyNoteUI(noteNumber, noteColor);

        // play sound or particle effect
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
}