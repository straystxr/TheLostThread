using System;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float force = 40f;
    [SerializeField] private float gravityEnhancer = 0.5f;
    private float damping;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

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
        //condition that when it is not an object with player early return will fire
        if (!other.CompareTag("Player")) return;
        
        //if rigidbody is null early return
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;
        
        //adding the force so its more floaty
        rb.AddForce(Vector3.up * force, ForceMode.Acceleration);
    }
}