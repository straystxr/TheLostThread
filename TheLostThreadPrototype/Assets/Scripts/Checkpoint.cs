using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private static Checkpoint lastTriggered;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (lastTriggered == this) return;

        CheckpointManager.Instance.SetCheckpoint(transform.position);
        Debug.Log("Checkpoint set at " + transform.position);
        lastTriggered = this;
    }
}
