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
        [SerializeField] private float radiusOfInteraction = 5f; //the radius of how far the object must be from the player
        
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.pink;
            Gizmos.DrawWireSphere(hands.position, radiusOfInteraction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            Debug.Log($"[E] inHand={(inHand != null)}, heldPlug={(heldPlug != null)}");
            //Started instead of Performed so it doesnt need to wait for a long time for the input as it did not take the response with Performed
            if (context.phase != InputActionPhase.Started) return;

            if (inHand != null && heldPlug != null)
            {
                Socket socket = socketsNearyby();
                Debug.Log($"socket={(socket != null ? socket.name : "NULL")}");
                if(socket != null)  Debug.Log($"{socket.CanAcceptPlug()}");
                
                //inserting and checking condition
                if (socket != null && socket.CanAcceptPlug() && heldPlug.plugorder == socket.order)
                {
                    //inHand.Release(); //removes isHeld to prevent it from still "holding" the plug
                    //firing insert socket method and will only insert with those that have socket script
                    insertSocket(socket);
                    socket.ConnectedPlug(heldPlug);
                    
                    //turning inhand and heldplug null
                    inHand = null;
                    heldPlug = null;

                    Interact?.Invoke(null);
                    return;
                }
            }
            
            if (inHand == null)
            {
                Socket socket = socketsNearyby();
                if (socket != null && socket.CanAcceptPlug())
                {
                    RemovePlugFromSocket(socket);
                    return;
                }
            }
            
            //If we're already holding an item/object there is no need to continue the method
            if (inHand != null)
            {
                transform.SetParent(null);
                // Only release if the object supports Release
                Debug.Log("no valid socket... dropping");
                inHand.Release();   //drops it 
                inHand = null; //making it null
                heldPlug = null; //making it null

                Interact?.Invoke(null);
                return;
            }
            Debug.Log("Interacting...");
            //creating a variable of the source and its position
            var origin = source.position;
            
            LayerMask interactableMask = ~LayerMask.GetMask("VentTransport"); // ignore transport triggers

            //var rayCasting = new RaycastHit[4]; //results of raycasting
            Collider[] colliders = new Collider[32]; //going for collider instead of raycasting
            int hitCounts = Physics.OverlapSphereNonAlloc(origin, radiusOfInteraction, colliders, interactableMask, QueryTriggerInteraction.Collide);
            
            //loop to recognize whether an item is pickupable or draggable
            for (int i = 0; i < hitCounts; i++)
            {
                Debug.Log($"InteractionHit: {colliders[i].name}");
                //trying to see if its hitting anything
                if (colliders[i].attachedRigidbody && colliders[i].attachedRigidbody.TryGetComponent(out IInteractable interactable))
                {
                    Debug.Log($"{interactable.GetType().Name} found!!");
                    //ignore if we’re already interacting with it
                    if (interactable == inHand) continue;
                    
                    //storing the object's data in hand
                    if (interactable.CanHold) inHand = interactable;
                    
                    // Check for Plug component FIRST, outside the currentSocket check
                    if (colliders[i].attachedRigidbody.TryGetComponent<Plug>(out var plug))
                    {
                        heldPlug = plug;  // assigning heldPlug
    
                        // Only remove from socket if it's actually in one
                        if (plug.currentSocket != null)
                        {
                            plug.currentSocket.RemovePlug();  // Use plug, not heldPlug
                        }
                    }

                    //handling interactable objects that can be held
                    if (interactable.CanHold) 
                    {
                        inHand = interactable;
                    }
                    
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
            Debug.Log("inserting socket...");
            //snapping the plugs with the socket
            Transform trans = heldPlug.transform;
            
            //removing parenting
            trans.SetParent(null);
            
            //updating the position to the socket's pos
            trans.position = socket.transform.position;
            trans.rotation = socket.transform.rotation;
            
            //parenting the plug to the socker
            trans.SetParent(socket.transform);
            
            Debug.Log($"Snapped Position: {trans.position}");  
            
            //removing the physics of the plug as it will stay snapped to socket by making kinematic true
            if(trans.TryGetComponent(out Rigidbody rb)) rb.isKinematic = true;
        }
        
        //removing plug
        private void RemovePlugFromSocket(Socket socket)
        {
            Plug plug = socket.currentPlug;
            socket.RemovePlug();

            if (plug == null) return;
            
            Debug.Log($"Removed Plug: {plug.GetType().Name}");
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
            //checking colliders to ensure there are sockets nearby (sockets each have their colliders)
            Collider[] colliders = new Collider[16];
            //checking w colliders whether object is an interactable and then checking if its a socket in loop
            int hitCounts = Physics.OverlapSphereNonAlloc(source.position, radiusOfInteraction, colliders);

            //similar as before just checking for sockets specifically through loop
            for (int i = 0; i < hitCounts; i++)
            {
                Socket socket = colliders[i].GetComponentInParent<Socket>();
                if (socket != null) return socket;
            }

            return null;
        }
    }
}
