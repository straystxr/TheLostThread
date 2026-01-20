using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class VentTransport : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public Transform exitPoint;
    public GameObject arrowUI; // optional, can stay

    [Header("Input")]
    public InputActionReference interactAction; // assign T

    private bool playerInside;
    private bool canTeleport = true;

    private Rigidbody playerRb;

    private void Awake()
    {
        playerRb = player.GetComponent<Rigidbody>();
        if (!playerRb)
            Debug.LogWarning("No Rigidbody on player! Teleport may fail.");
    }

    private void OnEnable()
    {
        interactAction.action.Enable();
        if (arrowUI) arrowUI.SetActive(false);
    }

    private void OnDisable()
    {
        interactAction.action.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = true;
        canTeleport = true;
        if (arrowUI) arrowUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = false;
        canTeleport = true;
        if (arrowUI) arrowUI.SetActive(false);
    }

    private void Update()
    {
        if (!playerInside) return;

        if (interactAction.action.triggered && canTeleport)
        {
            canTeleport = false; // prevent back-and-forth
            StartCoroutine(TeleportWithFade());
        }
    }

    private IEnumerator TeleportWithFade()
    {
        // Fade out
        if (ScreenFader.Instance != null)
            yield return ScreenFader.Instance.FadeOut(0.5f);

        // Teleport player
        if (playerRb != null)
            playerRb.position = exitPoint.position; // works with physics
        else
            player.transform.position = exitPoint.position;

        player.transform.rotation = exitPoint.rotation;

        // Fade in
        if (ScreenFader.Instance != null)
            yield return ScreenFader.Instance.FadeIn(0.5f);

        yield return new WaitForSeconds(0.1f);
        canTeleport = true; // allow next teleport
    }

    public void EnableTransport()
    {
        if (arrowUI)
            arrowUI.SetActive(true);
    }
}
