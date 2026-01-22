using UnityEngine;

public class InteractableProximity : MonoBehaviour
{
    private SimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponentInChildren<SimpleInteractable>();

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger with: " + other.name);
        
        if (other.CompareTag("Player"))
            interactable.ShowFeedback();
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited trigger with: " + other.name);
        
        if (other.CompareTag("Player"))
            interactable.HideFeedback();
    }
    
}