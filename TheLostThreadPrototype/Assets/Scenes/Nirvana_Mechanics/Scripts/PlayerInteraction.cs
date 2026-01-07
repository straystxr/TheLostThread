using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Nirvana_Mechanics.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInteraction : MonoBehaviour
    {
        //variables 
        [SerializeField] private Transform source; //source from where the object will be held
        [SerializeField] private float radiusOfInteraction = 0.5f; //the radius of how far the object must be from the player
        
        //object being held
        private Pickupable inHand;
        public void Interact(InputValue value)
        {
            //If we're already holding an item/object there is no need to continue the method
            if (inHand != null)
            {
                inHand.Drop();
                inHand = null;
                return;
            }
            Debug.Log("Interacting...");
            //creating a variable of the source and its position
            var origin = source.position;
            //var rayCasting = new RaycastHit[4]; //results of raycasting
            Collider[] colliders = new Collider[8];          // rename for clarity
            int hitCounts = Physics.OverlapSphereNonAlloc(origin, radiusOfInteraction,
                colliders);

            //getting number of hits
            //var hitCounts = Physics.OverlapSphereNonAlloc(origin, radiusOfInteraction, transform.forward, rayCasting);
            for (int i = 0; i < hitCounts; i++)
            {
                //trying to see if its hitting anything
                Debug.Log("Hit: " + colliders[i].name);
                Pickupable pickup = colliders[i].gameObject.GetComponent<Pickupable>();
                //within the loop it will check whether a pickupable was detected and then we will check whether
                //its null or not as the player will not be able to pick up all the objects within the game
                if (pickup != null)
                {
                    Debug.Log("Pickupable found!!");
                    pickup.Pickup(transform);
                    return;
                }
                
                var result = colliders[i]; //making the actual result readable
                if (result.attachedRigidbody == null) continue; //breaks the loop 
                
                //Look for attached RigidBody 
                if(!result.attachedRigidbody.TryGetComponent<Pickupable>(out var pickupable)) continue;
                Debug.Log(result);
                
                //pick up object
                inHand = pickupable; //lets the script know that an object is being held
                pickupable.Pickup(source);
                return; //breaks the function if an item was picked up
            }
            //in the case nothing it found
            Debug.Log("Object not found!");
        
        
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(source.position, radiusOfInteraction);
        }

        //code does not work without Update() check with the sir
        //using the update to see if the 'E' button is being detected at all by the player input system
        void Update()
        {
            if(Keyboard.current.eKey.wasPressedThisFrame)
                Interact(new InputValue());
        }
    }
}
