using System;
using UnityEngine;

public class CraneRotation : MonoBehaviour
{
    public float rotationSpeed = 60f;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, input * rotationSpeed * Time.deltaTime, Space.World);
    }
}