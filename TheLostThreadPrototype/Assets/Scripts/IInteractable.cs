using UnityEngine;

public interface IInteractable
{
    bool CanHold { get; }
    
    void Interact(Transform interactor);
    void Release();
}