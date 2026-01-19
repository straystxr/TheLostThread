using Unity.Cinemachine;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    private CinemachineTargetGroup targetGroup;
    [SerializeField] private LayerMask areaMask;

    private Collider areaCollider;

    private void Awake()
    {
        targetGroup = FindAnyObjectByType<CinemachineTargetGroup>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        var bitMask = 1 << other.gameObject.layer; // 2 to the power of another number
        if (bitMask == areaMask)
        {
            if (areaCollider != null)
                targetGroup.RemoveMember(areaCollider.transform);
            
            areaCollider = other;
            targetGroup.AddMember(areaCollider.transform, 0.5f, 1f);
        }
    }
    
    
}