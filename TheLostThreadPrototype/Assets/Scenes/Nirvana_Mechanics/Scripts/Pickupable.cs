using UnityEngine;

public class Pickupable : MonoBehaviour
{
    //checking whether object can be picked up by player which will be set as true for experimentation
    [SerializeField]private bool canPickUp = true;
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
