# TheLostThread

The Lost Thread is a game that features a stuffed bear who has been separated from its owner. The player must navigate through various environments to reunite the bear with its owner, overcoming obstacles and solving puzzles along the way.
The game's aim is the puzzle adventure aspect of it and to escape from the junkyard where the bear finds itself at the start of the game. If the player solves the puzzle, player escapes, else the player remained trapped in the junkyard.

## Features

The system includes the following features:
- Puzzle-solving mechanics that involves interacting with objects in the environment.
- A variety of environments to explore, each with its own unique challenges.
- Puzzles with environmental settings that require basic problem-solving skills to solve.

## Core Mechanics
The core mechanics of The Lost Thread include:
- Player Movement that has a state machine within it to include idle, walking, running, and jumping animation states.
- Object Interaction that allows the player to pick up, drag, or interact with objects within the environment that has 
a radius of interaction, and the player can only interact with one object at a time. 
- A sub system for a specific puzzle that is based of the object interaction mechanic, where the player has to pick up 
and match the objects with their corresponding slots to solve the puzzle using read-only script and conditions to check 
when it matches or when it doesn't match.

## System Architecture
- The game has two main core classes; Player Movements and Player Interactions/Object Interactions. Which then goes into
smaller core classes that specifically handles the various puzzles that we have spread out across the map, like the Crane Puzzle,
with each puzzle having its own unique script to handle the specific puzzle mechanics.
- Player Movement:
- Handles the movement of the player character, including walking, running, jumping, and idle states.
- Uses a state machine to manage the different movement states and transitions between them.
- Player movement includes the Player Input system found within the Unity Engine to handle input from the player similarly to Object Interaction.
- Player movements have variables such as; speed, jump height, rotation speed, Rigidbody components, Animator component, 
and playerInteraction derived from the PlayerInteraction.cs script 
## Player Movement Variables and Methods:
### Variables:
    [Header("Movement")]
    [SerializeField] private float NormalSpeed = 5f;
    [SerializeField] private float CarryingSpeed = 1.5f;
    [SerializeField] private float DraggingSpeed = 2.5f;
    [SerializeField] private float turnSpeed = 1080f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private PlayerState state;

    private Rigidbody myRigidbody;
    private Vector3 moveDirection;
    private PlayerInteraction playerInteraction;
    public Vector3 Direction
    {
    get { return moveDirection; }
    }
    private bool isGrounded;

### Methods:
    - private void OnCollisionStay(Collision collision){...}
    - private void OnCollisionExit(Collision collision){...}
    - private void OnJump(InputAction.CallbackContext context){...}
    - private void Awake(){...}
    - private void OnEnable(){...}
    - private void OnDisable(){...}
    - private void OnHandChanged(IInteractble obj){...}
    - private void FixedUpdate(){...}
    - private void NormalState(){...}
    - private void CarryingState(){...}
    - private void DraggingState(){...}
    - private void OnMove(InputAction.CallbackContext context){...}
### Player States Enum:
    - public enum PlayerState
    {
        None,
        Normal,
        Carrying,
        Dragging
    }
# Player Interaction Variables and Methods:
### Variables:
    - public event Action<IInteractable> Interact;
        
    - [Header("Player Position Settings")]
    //variables 
    - [SerializeField] private Transform source; //source from where the object will be held
    - [SerializeField] private Transform hands;
    - [SerializeField] private float radiusOfInteraction = 5f; //the radius of how far the object must be from the player
        
    - [Header("Current Held Object")]
        //object being held
        private IInteractable inHand;
        private PlayerInput playerInput;
        private Plug heldPlug;
        
        //draggable features
        private Draggable draggingHand;
### Methods:
    - private void Awake(){...}
    - private void Start(){...}
    - private void OnInteract(InputAction.CallbackContext context){...}
    - private void insertSocket(Socket socket){...}
    - private IInteractable RemovePlugFromSocket(Socket socket){...}
    - private void socketsNearby(){...}
# Data Structures Used
## Interfaces
- The game uses interfaces to define the behavior of interactable objects. The IInteractable interface defines methods for 
interacting with objects, such as PickUp, Drop, and Interact. This allows for different types of objects to implement their own 
interaction logic while adhering to a common interface.
- The IInteractable interface is implemented by various classes, such as Plug, Socket, and Draggable, each providing specific 
functionality for interacting with those objects.
  ```
  using UnityEngine;

  public interface IInteractable
  {
  bool CanHold { get; }
  void Interact(Transform interactor);
  void Release();
  }
  
  //enum to define the order of the plug
    public enum PlugOrder
    {
    first,
    second,
    third,
    fourth
    }
    
    public class Plug : MonoBehaviour
    {
    //script is used to store data only
    public PlugOrder plugorder;

    //bool is set to false immediately as the plugs will not be connected initially and will be used in other scripts
    public bool isConnected = false;
    public Scenes.Nirvana_Mechanics.Scripts.Socket currentSocket;
    }

    ```
  
## State Machines

     FixedUpdate(){
        if (state == PlayerState.Normal)
            NormalState();
        
        if (state == PlayerState.Carrying)
            CarryingState();
        
        if (state == PlayerState.Dragging)
            DraggingState();
     }
        private void NormalState()
        {
            // Movement
            Vector3 velocity = moveDirection.normalized * NormalSpeed;
            velocity.y = myRigidbody.linearVelocity.y;
            myRigidbody.linearVelocity = velocity;`
            // Rotation
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            Quaternion smoothRotation = Quaternion.RotateTowards(
                myRigidbody.rotation,
                targetRotation,
                turnSpeed * Time.fixedDeltaTime
        );

        myRigidbody.MoveRotation(smoothRotation);
    }

    private void CarryingState()
    {
        // Movement
        Vector3 velocity = moveDirection.normalized * CarryingSpeed;
        velocity.y = myRigidbody.linearVelocity.y;
        myRigidbody.linearVelocity = velocity;

        // Rotation
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        Quaternion smoothRotation = Quaternion.RotateTowards(
            myRigidbody.rotation,
            targetRotation,
            turnSpeed * Time.fixedDeltaTime
        );

        myRigidbody.MoveRotation(smoothRotation);
    }

    private void DraggingState()
    {
        // Movement
        Vector3 velocity = moveDirection.normalized * DraggingSpeed;
        velocity.y = myRigidbody.linearVelocity.y;
        myRigidbody.linearVelocity = velocity;
        
    }'
## If/Else Concept
- Throughout this game a lot of if/else statements are used to control the flow of the game. For example, in the object 
interaction mechanic, if the player is within the interaction radius of an object and presses the interact button, then 
the object will be picked up. Else, nothing will happen.
- If the player manages to place the metal object in the correct position in the puzzle with the crane, then the player 
will be able to proceed to the next area. Else, the player will have to keep trying to get the passcode correct without 
the rest of the hints provided or continuously die in the obstacle.
- If the player interacts with a socket while holding a plug, then the plug will be inserted into the socket. 
Else, if the player interacts with a socket while not holding anything, then the plug will be removed from the socket
- If the player solves the puzzle correctly then the player will be able to advance and turn on the fan, else they will
not be able to achieve the last fragment of the code. 
- If the player inputs the correct code in the office's keypad, then the door will open and the game will be completed,
else the door will remain locked and the player will stay stuck in the junkyard.
- If the player manages to collect all the notes spread out across the junkyard, then they will be rewarded with knowing 
what the code is, else they will have to figure it out on their own or through trial and error, increasing difficulty.


## Reflection on Programming Structure

**State Machines for Structured Control:**
We initially attempted to manage player behavior using booleans such as (`isWalking`, `isCarrying`, `isDragging`). 
This resulted in conflicting states where the animation system would trigger multiple clips simultaneously. 
Implementing an enum-based State Machine (`PlayerState`) with strict If/Else transition logic eliminated these race conditions. 
The structure enforces that the bear exists in only one state at a time, with transitions governed by clear conditional 
checks. This improved both reliability and readability of the movement code.

**Interface Abstraction for Modularity:**
Using `IInteractable` created a contract-based architecture. `PlayerInteraction` references the interface, not concrete 
classes, allowing Plugs, Sockets, and Draggables to be treated with polymorphism (Object Oriented Concept). This reduced 
coupling; we added new puzzle objects by implementing the interface rather than modifying existing interaction code, 
demonstrating how abstraction improves code reactivity to changing requirements.

**Separation of Concerns:**
Decoupling `PlayerMovement` (physics/animation) from `PlayerInteraction` (input/handling) meant modifying jump forces 
in `FixedUpdate()` never risked breaking pickup logic. The state machine served as the communication bridge, 
ensuring these systems coordinated through formal state changes rather than direct references, improving maintainability 
and reducing side effects.

## Link to Playable Game
https://i38waikiki.itch.io/the-lost-thread-proto?secret=T00Fg6DTDjSMy14LdFsgGFNPa0k
