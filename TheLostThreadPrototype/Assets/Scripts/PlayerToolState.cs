using UnityEngine;

public class PlayerToolState : MonoBehaviour
{
    public bool hasScrewdriver { get; private set; }

    public void PickupScrewdriver()
    {
        hasScrewdriver = true;
        Debug.Log("Player now HAS screwdriver");
    }
}