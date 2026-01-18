using System;
using UnityEngine;

public class SewageWater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Once the player hits the Sewage's collider it will die instantly
        Debug.Log("Trigger is working" +  other.gameObject.name);
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            Debug.Log("Water Detected");
            health.SewageDeath();
            Debug.Log($"Health Detected: {health}");
        }
        
    }
}
