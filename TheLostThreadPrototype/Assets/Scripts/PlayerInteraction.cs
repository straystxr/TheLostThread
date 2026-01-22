using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Nirvana_Mechanics.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInteraction : MonoBehaviour
    {
        public event Action<IInteractable> Interact;
        
        //variables 
        [SerializeField] private Transform source; //source from where the object will be held
        [SerializeField] private Transform hands;
        [SerializeField] private float radiusOfInteraction = 1f; //the radius of how far the object must be from the player
        
        //object being held
        private IInteractable inHand;
        private PlayerInput playerInput;
        
        //draggable features
        private Draggable draggingHand;
        
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            playerInput.ActivateInput();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            //Started instead of Performed so it doesnt need to wait for a long time for the input as it did not take the response with Performed
            if (context.phase != InputActionPhase.Started) return;
            
            
            //If we're already holding an item/object there is no need to continue the method
            if (inHand != null)
            {
                // Only release if the object supports Release
                inHand.Release();
                inHand = null;
                Interact?.Invoke(inHand);
                return;
            }
            Debug.Log("Interacting...");
            //creating a variable of the source and its position
            var origin = source.position;
            
            LayerMask interactableMask = ~LayerMask.GetMask("VentTransport"); // ignore transport triggers

            //var rayCasting = new RaycastHit[4]; //results of raycasting
            Collider[] colliders = new Collider[32]; //going for collider instead of raycasting
            int hitCounts = Physics.OverlapSphereNonAlloc(origin, radiusOfInteraction,
                colliders);

            //getting number of hits
            //var hitCounts = Physics.OverlapSphereNonAlloc(origin, radiusOfInteraction, transform.forward, rayCasting);
            //loop to recognize whether an item is pickupable or draggable
            for (int i = 0; i < hitCounts; i++)
            {
                //trying to see if its hitting anything
                Debug.Log("Hit: " + colliders[i].name);
                if (colliders[i].attachedRigidbody && colliders[i].attachedRigidbody.TryGetComponent(out IInteractable interactable))
                {
                    // Ignore if we’re already interacting with it
                    if (interactable == inHand) continue;
                    
                    //storing the object's data in hand
                    inHand = interactable;
                    Debug.Log($"{interactable.GetType().Name} found!!");
                    //the object will be held from the source aka hands
                    interactable.Interact(hands);
                    Interact?.Invoke(inHand);
                    return;
                }
            }
            //in the case nothing is found
            Debug.Log("Object not found!");
        }
        
        

       /* private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(source.position, radiusOfInteraction);
        }/*

        //code does not work without Update() check with the sir
        //using the update to see if the 'E' button is being detected at all by the player input system
       /* void Update()
        {
            if(Keyboard.current.eKey.wasPressedThisFrame)
                Interact(new InputValue());
        } */
    }
}
