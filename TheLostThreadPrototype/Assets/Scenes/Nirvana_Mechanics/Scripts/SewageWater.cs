using System;
using UnityEngine;

public class SewageWater : MonoBehaviour
{
    //ontriggerenter is used as it will trigger once since the death will be instead once the player
    //collides w it
    private void OnTriggerEnter(Collider other)
    {
        //Once the player hits the Sewage's collider it will die instantly
        Debug.Log("Trigger is working" +  other.gameObject.name);
        //calling the health script to get data and putting it in a variable called health
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            Debug.Log("Water Detected");
            //calling the function for instant death
            health.SewageDeath();
            Debug.Log($"Health Detected: {health}");
        }
        
    }
}
