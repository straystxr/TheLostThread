using UnityEngine;

public interface IInteractable
{
    void Interact(Transform interactor);
    void Release();

    
    void OnFocus();
    void OnUnfocus();
}