using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform cameraTransform;

        private void Start()
        {
            cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(cameraTransform);
        }
    }
}