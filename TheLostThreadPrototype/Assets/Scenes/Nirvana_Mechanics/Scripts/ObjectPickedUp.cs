using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class ObjectPickedUp : MonoBehaviour
{
    //variables 
    [SerializeField] private Transform source;
    [SerializeField] private float radiusOfInteraction = 1f;
    public void ObjectInteraction(InputValue value)
    {
        //creating a variable of the source and its position
        var origin = source.position;
        var raycasting = new RaycastHit[4]; //results of raycasting
        
        //getting number of hits
        var hitCounts = Physics.SphereCastNonAlloc(origin, radiusOfInteraction, transform.forward, raycasting);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(source.position, radiusOfInteraction);
    }
}
