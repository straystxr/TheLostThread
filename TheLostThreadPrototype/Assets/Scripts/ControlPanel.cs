using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlPanel : MonoBehaviour, IInteractable
{
    [SerializeField] private CinemachineCamera craneCam;
    [SerializeField] private CinemachineCamera defaultCam;
    
    public void Interact(Transform interactor)
    {
        var playerInput = PlayerInput.all.First();
        playerInput.SwitchCurrentActionMap("Crane");
        
        // do camera stuff here.
        craneCam.Prioritize();
    }

    public void Release()
    {
        defaultCam.Prioritize();
    }
}