using System;
using UnityEngine;

public class MagnetMovement : MonoBehaviour
{
    public float speed = 5f; // Movement speed :)
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from keyboard
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down

        // Create a movement vector
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // Normalize to prevent faster diagonal movement
        if (movement.magnitude > 1f)
            movement = movement.normalized;

        // Move the object
        rb.linearVelocity = movement * speed;
    }
    
}