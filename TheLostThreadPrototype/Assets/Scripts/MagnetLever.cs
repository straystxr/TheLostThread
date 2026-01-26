using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class MagnetLever : MonoBehaviour, IInteractable
{
    [Header("Camera")]
    public CinemachineCamera leverCam;
    public CinemachineCamera playerCam;

    [Header("Player")]
    public GameObject player;

    [Header("Crane / Magnet Controller")]
    public CraneRotation craneRotation;   // drag the object that has CraneRotation on it

    [Header("Input")]
    public InputActionReference cancelAction; // ESC

    [Header("Feedback")]
    public GameObject glowObject;
    public GameObject pressEUI;

    [Header("Lever")]
    public GameObject lever;

    private bool isInteracting = false;

    private void OnEnable()
    {
        if (cancelAction != null)
            cancelAction.action.Enable();
    }

    private void OnDisable()
    {
        if (cancelAction != null)
            cancelAction.action.Disable();
    }

    private void Update()
    {
        if (!isInteracting) return;

        if (cancelAction != null && cancelAction.action.triggered)
        {
            ExitInteraction();
        }
    }

    public void Interact(Transform interactor)
    {
        if (isInteracting) return;
        isInteracting = true;

        TurnOn();
        
        // Lock player movement
        var movement = player.GetComponent<PlayerMovement>();
        if (movement) movement.enabled = false;

        // Enable crane rotation controls
        if (craneRotation)
            craneRotation.canControl = true;

        // Switch action map to Crane
        var playerInput = player.GetComponent<PlayerInput>();
        if (playerInput) playerInput.SwitchCurrentActionMap("Crane");


        // Hide glow + UI
        if (glowObject) glowObject.SetActive(false);
        if (pressEUI) pressEUI.SetActive(false);

        // Switch cameras
        if (leverCam && playerCam)
        {
            leverCam.Priority = 20;
            playerCam.Priority = 10;
        }
        
    }

    public void Release()
    {
        ExitInteraction();
    }

    public void ExitInteraction()
    {
        if (!isInteracting) return;
        isInteracting = false;

        // Unlock player movement
        var movement = player.GetComponent<PlayerMovement>();
        if (movement) movement.enabled = true;

        // Disable crane rotation controls
        if (craneRotation)
            craneRotation.canControl = false;

        // Switch action map back to Player
        var playerInput = player.GetComponent<PlayerInput>();
        if (playerInput) playerInput.SwitchCurrentActionMap("Player");


        // Show glow + UI again
        if (glowObject) glowObject.SetActive(true);
        if (pressEUI) pressEUI.SetActive(true);

        // Switch cameras back
        if (leverCam && playerCam)
        {
            leverCam.Priority = 5;
            playerCam.Priority = 15;
        }
    }

    private void TurnOn()
    {
        if (!lever) return;

        Animation anim = lever.GetComponent<Animation>();
        if (anim) anim.Play();

        AudioSource audio = lever.GetComponent<AudioSource>();
        if (audio) audio.Play();
    }
    
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.started) return;     // important
        if (!isInteracting) return;

        ExitInteraction();
    }

}
