using UnityEngine;

public class ScrewdriverPickup : MonoBehaviour, IInteractable
{
    public void Interact(Transform interactor)
    {
        Debug.Log("Screwdriver Interact called");

        PlayerToolState tools = interactor.GetComponentInParent<PlayerToolState>();
        if (!tools)
        {
            Debug.LogError("NO PlayerToolState FOUND IN PARENT");
            return;
        }

        tools.PickupScrewdriver();
        gameObject.SetActive(false);
    }

    public void Release() { }
}