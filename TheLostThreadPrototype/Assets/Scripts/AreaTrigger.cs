using Unity.Cinemachine;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    private CinemachineTargetGroup targetGroup;

    [SerializeField] private LayerMask areaMask;

    private Transform currentArea;

    private void Awake()
    {
        targetGroup = FindAnyObjectByType<CinemachineTargetGroup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & areaMask) == 0)
            return;

        // Remove previous area
        if (currentArea != null)
        {
            targetGroup.RemoveMember(currentArea);
        }

        // Set new area
        currentArea = other.transform;
        targetGroup.AddMember(currentArea, 0.5f, 1f);

        // Save checkpoint
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.SetCheckpoint(transform.position);
        }
    }
}
