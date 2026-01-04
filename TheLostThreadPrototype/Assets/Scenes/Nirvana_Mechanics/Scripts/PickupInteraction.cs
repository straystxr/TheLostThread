using UnityEngine;

public class PickupInteraction : MonoBehaviour
{
    //in this class we will have the collisions actually happening but no motion will occur
    private Rigidbody rb;
    
    //once the game starts we will need to get the component of RigidBody
    private void Awake()
    {
        //rb will store the component of rigidbody
        rb = GetComponent<Rigidbody>();
    }

    public void Pickup(Transform target)
    {
        Debug.Log("Picked up");
    }
}
