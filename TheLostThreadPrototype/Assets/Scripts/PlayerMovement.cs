using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour

{
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 1080f;
    
    
    private Rigidbody myRigidbody;
    private float xRotation = 0f;

    private Vector3 direction;
    
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }
    

    private void FixedUpdate()
    {
        // Movement with velocity
        Vector3 velocity = direction * moveSpeed;
        velocity.y = myRigidbody.linearVelocity.y;
        myRigidbody.linearVelocity = velocity;
        myRigidbody.angularVelocity = Vector3.zero; // stops friction
        
        if (direction.magnitude <= Mathf.Epsilon) return; 
        
       var currentRotation = myRigidbody.rotation;
       var targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
       var rotation = Quaternion.RotateTowards(currentRotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
           
       myRigidbody.MoveRotation(rotation);
    }
    
    private void OnMove(InputValue value)
    {
        var valueVector = value.Get<Vector2>();
        direction = new Vector3(valueVector.x, 0, valueVector.y);
    }
}