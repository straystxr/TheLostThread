using System;
using Scenes.Nirvana_Mechanics.Scripts;
using UnityEngine;

public class Fan : MonoBehaviour
{
    //read only bool
    private bool FanActive =>
        PuzzleManager.Instance != null &&
        PuzzleManager.Instance.allPlugsConnected;

    
    [Header("PHYSICS")]
    [SerializeField] private float force = 40f;
    [SerializeField] private float gravityEnhancer = 0.5f;
    private float damping;
    
    [Header("VISUALS")]
    [SerializeField] private ParticleSystem fanParticles; //adding particle effects

    private void OnTriggerEnter(Collider other)
    {
        //early return for when fan is not active
        if (!FanActive) return;
        
        //if the tag on the game objetct is not player early return
        if (!other.CompareTag("Player")) return;

        //adding rigidbody so we have something to apply physics to and if it there is no rigidbody
        //early return
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        //removing Gravity to later on make it more floaty in triggerStay
        damping = rb.linearDamping;
        rb.linearDamping = 2f;
        Debug.Log("Fan On");
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        // Restore gravity
        rb.linearDamping = damping;
    }

    private void OnTriggerStay(Collider other)
    {
        //early return for when fan is not active
        if (!FanActive) return;
        //condition that when it is not an object with player early return will fire
        if (!other.CompareTag("Player")) return;
        
        //if rigidbody is null early return
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;
        
        //adding the force so its more floaty
        rb.AddForce(Vector3.up * force, ForceMode.Acceleration);
    }
}