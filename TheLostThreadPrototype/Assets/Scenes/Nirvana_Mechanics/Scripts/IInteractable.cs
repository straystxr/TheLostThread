using UnityEngine;

public interface IInteractable
{
    void Interact(Transform interactor);
    void Release();
<<<<<<< Updated upstream
=======
    
    void OnFocus();
    void OnUnfocus();
>>>>>>> Stashed changes
}