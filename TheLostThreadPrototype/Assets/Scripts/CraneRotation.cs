using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraneRotation : MonoBehaviour
{
    public float rotationSpeed = 60f;
    private float input;
    
    public bool canControl = false;

    void Update()
    {
       // var euler = transform.localEulerAngles;
       // euler.y += input * rotationSpeed * Time.deltaTime;
       // euler.y = Mathf.Clamp(euler.y, 45f, 135f);
     
       // transform.localEulerAngles = euler;

       if (!canControl) return;
        transform.Rotate(Vector3.up, input * rotationSpeed * Time.deltaTime, Space.World);
    }

    public void OnMoveCrane(InputAction.CallbackContext context)
    {
        input = context.ReadValue<float>();
    }
    
}