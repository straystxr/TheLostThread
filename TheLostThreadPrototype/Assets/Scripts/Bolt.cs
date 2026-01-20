using UnityEngine;

public class Bolt : MonoBehaviour
{
    private bool removed = false;

    public void TryRemove(PlayerToolState tools)
    {
        if (removed) return;

        if (!tools || !tools.hasScrewdriver)
        {
            Debug.Log("Bolt: no screwdriver");
            return;
        }

        removed = true;
        Debug.Log("Bolt removed");
        gameObject.SetActive(false);
    }
}