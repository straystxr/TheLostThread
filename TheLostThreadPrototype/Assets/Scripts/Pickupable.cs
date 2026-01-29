using System;
using UnityEngine;

//this is going to be used to set the object picked up as kinemtic or non kinematic
[RequireComponent(typeof(Rigidbody))]
public class Pickupable : MonoBehaviour, IInteractable
{
    //checking whether object can be picked up by player which will be set as true for experimentation
    [SerializeField]private bool canPickUp = true;
    //in this class we will have the collisions actually happening but no motion will occur
    public Rigidbody rb;
    private bool isHeld = false;
    
    //once the game starts we will need to get the component of RigidBody
    private void Awake()
    {
        //rb will store the component of rigidbody
        rb = GetComponent<Rigidbody>();
    }

    /*
    private void FixedUpdate()
    {
        //no parent detected == no movement
        if (!isHeld || transform.parent == null) return;
        //adding a variable to store the parent's position
        var target = transform.parent;
        //moving the object in accordance to the player/source
        rb.MovePosition(target.position);
        rb.MoveRotation(target.rotation);
        //Debug.Log($"Target position is " + target.position);
    }*/

    public bool CanHold => true;

    public void Interact(Transform target)
    {
        //early return if its already held
        if (isHeld) return;
        
        //if its not a pickupable it will return not allowing it to pickup
        if (!canPickUp) return;
        //else it will be debug picked up
        Debug.Log("Picked up");
        //setting the parent as the target parameter
        transform.SetParent(target);
        transform.localPosition = Vector3.zero; // Center it on hold point
        transform.localRotation = Quaternion.identity;
    
        rb.isKinematic = true;
        isHeld = true;
    }
    
    //Drop function
    public void Release()
    {
        if (!isHeld) return;
        //reverting all the changes basically
        Debug.Log("Dropped");
        transform.SetParent(null);
        rb.isKinematic = false;
        isHeld = false;
        
        //rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //Implementing a glow effect
    public void OnFocus() { }
    public void OnUnfocus() { }
}
