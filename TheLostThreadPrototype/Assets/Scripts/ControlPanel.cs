using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlPanel : MonoBehaviour, IInteractable
{
    [SerializeField] private CraneRotation crane;        // new
    [SerializeField] private CinemachineCamera craneCam;
    [SerializeField] private CinemachineCamera defaultCam;

    public bool CanHold => false;

    public void Interact(Transform interactor)
    {
        var playerInput = PlayerInput.all.First();
        playerInput.SwitchCurrentActionMap("Crane");
        
        // Enable crane control
        crane.canControl = true;  // new
        
        // do camera stuff here.
        craneCam.Prioritize();
    }

    public void Release()
    {
        
        // Disable crane control
        crane.canControl = false; // new
        
        defaultCam.Prioritize();
        
        // Switch player input back to default player map
        var playerInput = PlayerInput.all.First(); // new
        playerInput.SwitchCurrentActionMap("Player"); // new
    }
}