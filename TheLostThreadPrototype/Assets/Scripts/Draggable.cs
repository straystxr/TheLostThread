using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Draggable : MonoBehaviour, IInteractable
{
    [Header("DRAGGABLE SETTINGS")]
    //Dragging Physics Variables
    //checking whether object can be picked up by player which will be set as true for experimentation
    [SerializeField]private bool canBeDragged = true; 
    //strength of pull/push
    [SerializeField] private float dragForce =4f;
    //to remove wobble
    [SerializeField] private float wobbleForce = 4f; 
    //if the player is too far it will stop being dragged
    [SerializeField] private float maxDistance = 10f; 
    
    [Header("COMPONENTS")]
    //in this class we will have the collisions actually happening but no motion will occur
    public Rigidbody rb;
    //player component aka source of who's dragging the object
    private Transform player;
    //an offset so the object is not in the player
    private Vector3 objectOffset = new Vector3(0f, 2f, 2f);
    
    //once the game starts we will need to get the component of RigidBody
    private void Awake()
    {
        //rb will store the component of rigidbody
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //early return if no player is detected
        if (player == null) return;
        //variable created to store the object's point in correlation to the player with an offset
        Vector3 point = player.TransformPoint(objectOffset);
        Debug.Log(point);
        /*adding a constraint of distance to max distance meaning if the number of the distance exceeds maxDistance 
        release function will be fired and an early return will be done*/
        float distance = Vector3.Distance(transform.position, point);
        if (distance > maxDistance)
        {
            Release(); return;
        }

        Vector3 force = (point - transform.position) * dragForce
                        - rb.linearVelocity * wobbleForce;
        rb.AddForce(force, ForceMode.Acceleration);
        
    }

    public bool CanHold => true;

    public void Interact(Transform target)
    {
        //if its not a draggable it will have an early return not allowing it to be dragged
        if (!canBeDragged) return;
        
        player = target;
        //else it will be debug.log to picked up
        Debug.Log("Dragging");
        //setting the parent as the target parameter
        //transform.SetParent(target);
        //making sure Kinematic setting is off as we want the object to have physics on it
        rb.isKinematic = false;
        rb.transform.parent = target;
    }
    
    //Drop function
    public void Release()
    {
        //reverting all the changes basically
        Debug.Log("Dropped");
        player = null;
        rb.isKinematic = false;
        //rb.transform.parent = null;
    }
    
    // Implementing a glow effect
    public void OnFocus() { }
    public void OnUnfocus() { }

}