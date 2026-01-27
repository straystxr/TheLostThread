using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Nirvana_Mechanics.Scripts
{
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
        private Plug heldPlug;
        
        //draggable features
        private Draggable draggingHand;
        
        private void Awake()
        {
            playerInput = PlayerInput.all.First();
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
        
        //adding inserting plugs + removing plugs interactions
        //if plug == correctSocket plug will snap to the correct socket
        //if plug != correctSocket plug will not snap and will be removed
        private void insertSocket(Socket socket)
        {
            //removing it from hand as the position will now be updated to snap to the wall
            inHand.Release();
            
            //snapping the plugs with the socket
            Transform trans = heldPlug.transform;
            trans.position = socket.transform.position;
            trans.rotation = socket.transform.rotation;
            //need to fire the function somewhere
            
            Debug.Log($"Snapped Position: {trans.position}");  
            
            //removing the physics of the plug as it will stay snapped to socket by making kinematic true
            if(trans.TryGetComponent(out Rigidbody rb)) rb.isKinematic = true;
        }
        
        //removing plug
        private void RemovePlugFromSocket(Socket socket)
        {
            Plug plug = socket.currentPlug;
            socket.RemovePlug();

            //if its not a plug get interactable components and early return to not change anything
            if (!plug.TryGetComponent(out IInteractable interactable)) return;

            inHand = interactable;
            heldPlug = plug;

            interactable.Interact(hands);
            Interact?.Invoke(inHand);
        }
        
        //when E is fired if the object in hand is a plug this method will fire to check if there are
        //any sockets nearby
        private Socket socketsNearyby()
        {
            Collider[] colliders = new Collider[16];
            //checking w colliders whether object is an interactable and then checking if its a socket in loop
            int hitCounts = Physics.OverlapSphereNonAlloc(source.position, radiusOfInteraction, colliders);

            for (int i = 0; i < hitCounts; i++)
            {
                //looping through sockets 
                Socket socket = colliders[i].GetComponent<Socket>();
                if (socket != null) return socket; //if socket alr has a plug it will fire an early return
            }

            return null;
        }
    }
}
