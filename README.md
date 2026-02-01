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
## If/Else Concept
- Throughout this game a lot of if/else statements are used to control the flow of the game. For example, in the object 
interaction mechanic, if the player is within the interaction radius of an object and presses the interact button, then 
the object will be picked up. Else, nothing will happen.
- If the player manages to place the metal object in the correct position in the puzzle with the crane, then the player 
will be able to proceed to the next area. Else, the player will have to keep trying to get the passcode correct without 
the rest of the hints provided or continuously die in the obstacle.

## Reflection Throughout Development
Throughout development, we faced several challenges that required us to adapt and learn new skills. One of the main 
challenges was implementing the puzzle mechanics in a way that was both engaging and intuitive for players. 
We had to experiment with different designs and gather feedback to refine the puzzles.
Another challenge we unknowningly faced was the map size hindering the interactions making it difficult for players to 
find and interact with objects.
Lastly merge conflicts was a very common issue that we faced as we did not really know how to optimize it well as well as
considering that we were all new to using version control systems like Github. We learned the importance of clear communication and
coordination when working in a team environment.
Overall, the development of The Lost Thread was a valuable learning experience that taught us the importance of
adaptability, problem-solving, and teamwork in game development.