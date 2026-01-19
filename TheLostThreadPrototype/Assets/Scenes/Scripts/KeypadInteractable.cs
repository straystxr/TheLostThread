using UnityEngine;

public class KeypadInteractable : StaticInteractable
{
    public GameObject keypadCamera;

    public override void Interact(Transform interactor)
    {
        CameraManager.Instance.EnterInteraction(keypadCamera);
    }

    public override void Release()
    {
        CameraManager.Instance.ExitInteraction();
    }
}