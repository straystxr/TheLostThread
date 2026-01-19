using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public GameObject thirdPersonCam;
    private GameObject activeInteractCam;

    void Awake()
    {
        Instance = this;
    }

    public void EnterInteraction(GameObject cam)
    {
        thirdPersonCam.SetActive(false);
        activeInteractCam = cam;
        cam.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitInteraction()
    {
        if (activeInteractCam)
            activeInteractCam.SetActive(false);

        thirdPersonCam.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}