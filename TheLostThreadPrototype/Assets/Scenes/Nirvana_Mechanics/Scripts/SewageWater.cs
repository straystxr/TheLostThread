using System;
using UnityEngine;

public class SewageWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Once the player hits the Sewage's collider it will die instantly
        if (other.TryGetComponent<Health>(out var health))
        {
            Debug.Log("Water detected");
            health.SewageDeath();
        }
        
    }
}
