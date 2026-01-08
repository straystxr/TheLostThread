using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MagnetTarget : MonoBehaviour
{
    private const string MetalTag = "Metal";

    private Rigidbody rb;
    private MagnetForce magnetForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // There is no magnet overlapping me
        if (magnetForce == null) return;
        
        // We might have disabled the collider
        if (!magnetForce.IsActive)
        {
            magnetForce = null;
            return;
        }
        
        // Direction = from the metal object → Electromagnet's center
        Vector3 pullDirection = (magnetForce.transform.position - transform.position).normalized;
        
        rb.AddForce(pullDirection * magnetForce.ForceStrength, ForceMode.Force);

        if (magnetForce.Velocity.magnitude > 0.1f)
        {
            rb.AddForce(magnetForce.Velocity, ForceMode.Force);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(MetalTag) && other.TryGetComponent(out magnetForce))
        {
            Debug.Log("Found a magnet");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Other needs to be Metal
        // Other needs to have MagnetForce as a script
        // AND other MagnetForce matches our MagnetForce
        if (other.CompareTag(MetalTag) && other.TryGetComponent<MagnetForce>(out var magnet) && magnetForce == magnet)
        {
            Debug.Log("I don't love you anymore :'(");
            magnetForce = null;
        }
    }
}