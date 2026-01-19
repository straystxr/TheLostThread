
using System;
using UnityEngine;

public class MagnetForce : MonoBehaviour
{
    [SerializeField] private float forceStrength = 20f;

    public bool IsActive => attachedCollider.enabled;
    
    public float ForceStrength => forceStrength;
    
    public Vector3 Velocity => attachedCollider && attachedCollider.attachedRigidbody
        ? attachedCollider.attachedRigidbody.linearVelocity
        : Vector3.zero;
    
    // If we deactivate the collider, we return a zero, otherwise we send back how fast it's moving
    
   /* detached momentarily!!
    public Vector3 Velocity => attachedCollider 
        ? attachedCollider.attachedRigidbody.linearVelocity
        : Vector3.back;
*/

    
    private Collider attachedCollider;
    
    private void Awake()
    {
        attachedCollider = GetComponent<Collider>();
        if (attachedCollider != null)
            attachedCollider.enabled = false;
    }
    
    private void Update()
    {
        // Toggle magnet on/off with E key
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Enable/disable the collider
            attachedCollider.enabled = !attachedCollider.enabled;
            Debug.Log("Magnet " + (attachedCollider.enabled ? "Activated" : "Deactivated"));

            if (attachedCollider.enabled)
            {
                // Optional: prevent metal from being pushed out at start
                Collider[] metals = Physics.OverlapCapsule(
                    attachedCollider.bounds.center - Vector3.up * attachedCollider.bounds.extents.y,
                    attachedCollider.bounds.center + Vector3.up * attachedCollider.bounds.extents.y,
                    ((CapsuleCollider)attachedCollider).radius
                );

                foreach (Collider metal in metals)
                {
                    if (metal.CompareTag("Metal"))
                    {
                        Physics.IgnoreCollision(attachedCollider, metal, true);
                    }
                }
            }
        }
    }

    
    /* disabled for the moment!!!
     private void Update()
     {
         // Toggle magnet on/off with E key
         if (Input.GetKeyDown(KeyCode.E))
         {
             attachedCollider.enabled = !attachedCollider.enabled;
             Debug.Log("Magnet " + (attachedCollider.enabled ? "Activated" : "Deactivated"));
         }
     }
     */
}












/* Fixed Joint feature
private void OnTriggerEnter(Collider other)
{
    if (!other.CompareTag(targetTag)) return;

    Rigidbody metalRb = other.GetComponent<Rigidbody>();
    Rigidbody electromagnetRb = electromagnet.GetComponent<Rigidbody>();

    if (metalRb != null && electromagnetRb != null && other.GetComponent<FixedJoint>() == null)
    {
        // Attach the metal to the electromagnet using a FixedJoint
        FixedJoint joint = other.gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = electromagnetRb;
        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;

        // Move the metal to the bottom of the electromagnet
        other.transform.position = electromagnet.position - new Vector3(0, stickOffsetY, 0);

        // Optional: prevent metal from rotating
        metalRb.freezeRotation = true;
    }
}
*/

 

/* Version 3, with Lift Smoothing- Acceleration Ease-In function:
 
 using UnityEngine;
   
   public class MagnetForce : MonoBehaviour
   {
       public float maxForce = 30f;         // Maximum pull force at full strength
       public float smoothTime = 0.5f;      // Larger = slower acceleration
       public string targetTag = "Metal";
   
       private Transform electromagnet;     // Parent transform (Electromagnet)
   
       private void Start()
       {
           electromagnet = transform.parent;
       }
   
       private void OnTriggerStay(Collider other)
       {
           if (!other.CompareTag(targetTag)) return;
   
           Rigidbody rb = other.GetComponent<Rigidbody>();
           if (rb == null) return;
   
           // Direction from object -> electromagnet
           Vector3 direction = (electromagnet.position - other.transform.position).normalized;
   
           // Distance (used for smooth acceleration)
           float distance = Vector3.Distance(other.transform.position, electromagnet.position);
   
           // Compute a smooth force factor (0 → 1)
           // Closer = stronger pull, but smoothed over time
           float forceFactor = Mathf.SmoothStep(0f, 1f, Time.deltaTime / smoothTime);
   
           // Final force
           Vector3 force = direction * (maxForce * forceFactor);
   
           rb.AddForce(force, ForceMode.Force);
       }
   }
   
 */



/* Magnet Force Version 2 (super simple!?):
 
using UnityEngine;
 
public class MagnetForce : MonoBehaviour
{
    public string targetTag = "Metal"; // Tag to identify which objects will be pulled up
    public float forceStrength = 20f;  // Strength of the movement force
    public Vector3 movementDirection = Vector3.forward; // Direction to move objects (can be any direction, e.g. Vector3.forward)

    private void OnTriggerStay(Collider other)
    {
        // Check if the object inside the cylinder has the correct tag
        if (other.CompareTag(targetTag))
        {
            // Get the Rigidbody component of the object
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply force in the defined direction
                rb.AddForce(movementDirection.normalized * forceStrength, ForceMode.Force);
            }
        }
    }
}
*/


/* VERSION 4, CRAP

using UnityEngine;
   
   public class MagnetForce : MonoBehaviour
   {
       public float attractionSpeed = 5f;
       public string targetTag = "Metal";
       public float stickOffsetY = 0.5f; // How far below the magnet the metal should stick
   
       private Transform electromagnet;
   
       private void Start()
       {
           electromagnet = transform.parent;
       }
   
       private void FixedUpdate()
       {
           // Find all metals in range (or use your collider trigger logic)
           Collider[] metals = Physics.OverlapSphere(transform.position, 5f); // 5 units radius
           foreach (Collider other in metals)
           {
               if (!other.CompareTag(targetTag)) continue;
   
               Rigidbody rb = other.GetComponent<Rigidbody>();
               if (rb == null) continue;
   
               // Compute target position (bottom of magnet)
               Vector3 targetPos = electromagnet.position - new Vector3(0, stickOffsetY, 0);
   
               // Smoothly move the metal toward the target
               rb.MovePosition(Vector3.MoveTowards(rb.position, targetPos, attractionSpeed * Time.fixedDeltaTime));
           }
       }
   }
   */