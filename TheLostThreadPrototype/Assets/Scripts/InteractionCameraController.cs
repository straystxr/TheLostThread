using UnityEngine;

public class InteractionCameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform focusPoint; // point to look at
    public float zoomSpeed = 5f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isZoomed = false;

    void Start()
    {
        if (!mainCamera) mainCamera = Camera.main;
        originalPosition = mainCamera.transform.position;
        originalRotation = mainCamera.transform.rotation;
    }

    void Update()
    {
        if (!mainCamera) return;

        if (isZoomed)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, focusPoint.position, Time.deltaTime * zoomSpeed);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, focusPoint.rotation, Time.deltaTime * zoomSpeed);
        }
        else
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalPosition, Time.deltaTime * zoomSpeed);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, originalRotation, Time.deltaTime * zoomSpeed);
        }
    }

    public void ZoomIn() => isZoomed = true;
    public void ZoomOut() => isZoomed = false;
}