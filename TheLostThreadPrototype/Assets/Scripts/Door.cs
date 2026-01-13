using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform openPosition;
    [SerializeField] private float openSpeed = 2f;

    private bool isOpen = false;

    public void OpenDoor()
    {
        isOpen = true;
    }

    private void Update()
    {
        if (!isOpen) return;

        transform.position = Vector3.Lerp(
            transform.position,
            openPosition.position,
            Time.deltaTime * openSpeed
        );
    }
}