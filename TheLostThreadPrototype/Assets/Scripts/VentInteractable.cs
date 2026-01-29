using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class VentInteractable : MonoBehaviour, IInteractable
{
    [Header("Cameras")]
    public CinemachineCamera ventCam;
    public CinemachineCamera playerCam;

    [Header("Player")]
    public GameObject player;

    [Header("Vent")]
    public Rigidbody ventGrid;
    public Bolt[] bolts;

    [Header("Input")]
    public InputActionReference clickAction;
    public InputActionReference cancelAction;
    
    [Header("Next Step")]
    public VentTransport transport; // DRAG THIS
   
    public bool IsOpen()
    {
        return isOpen; // returns true if all bolts removed
    }
    
    private bool ventOpened = false;
    private bool isInteracting;
    private PlayerToolState tools;
    private Rigidbody playerRb;
    private PlayerMovement playerMovement;
    
    private bool isOpen = false; // starts closed


    private void Awake()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        playerRb = player.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        clickAction.action.Enable();
        cancelAction.action.Enable();
    }

    private void OnDisable()
    {
        clickAction.action.Disable();
        cancelAction.action.Disable();
    }

    private void Update()
    {
        if (!isInteracting) return;

        if (cancelAction.action.triggered)
        {
            ExitVent();
            return;
        }

        if (clickAction.action.triggered)
        {
            HandleClick();
        }
    }

    public bool CanHold => false;

    public void Interact(Transform interactor)
    {
        if (isOpen)
        {
            Debug.Log("Vent already open, skip VentInteractable logic");
            return; // skip everything so E can be handled by VentTransport
        }

        if (isInteracting || ventOpened) return;

        tools = interactor.GetComponentInParent<PlayerToolState>();
        if (!tools || !tools.hasScrewdriver)
        {
            Debug.Log("Vent requires screwdriver");
            return;
        }

        EnterVent();
    }

    

    void EnterVent()
    {
        isInteracting = true;

        // Disable movement script FIRST
        if (playerMovement)
            playerMovement.enabled = false;

        // Stop physics safely
        playerRb.linearVelocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
        playerRb.isKinematic = true;

        // Cursor ON
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Camera switch
        ventCam.Priority = 20;
        playerCam.Priority = 10;

        // Block other interactions
        player.GetComponent<PlayerInteractionState>().isBusy = true;
    }

    void ExitVent()
    {
        isInteracting = false;

        // Re-enable physics
        playerRb.isKinematic = false;

        // Re-enable movement
        if (playerMovement)
            playerMovement.enabled = true;

        // Cursor OFF
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Camera back
        ventCam.Priority = 5;
        playerCam.Priority = 15;

        player.GetComponent<PlayerInteractionState>().isBusy = false;
    }

    void HandleClick()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 1f);
        int boltLayer = LayerMask.GetMask("Bolt");

        if (Physics.Raycast(ray, out RaycastHit hit, 5f, boltLayer))
        {
            Debug.Log("Hit: " + hit.collider.name);

            Bolt bolt = hit.collider.GetComponent<Bolt>();
            if (bolt)
            {
                Debug.Log("Bolt hit!");
                bolt.TryRemove(tools);
                CheckBolts();
            }
        }
        else
        {
            Debug.Log("Raycast hit NOTHING");
        }
    }
    
    void CheckBolts()
    {
        foreach (var bolt in bolts)
        {
            if (bolt.gameObject.activeSelf) return;
        }

        // Vent is now open
        ventGrid.isKinematic = false;
        isOpen = true;

        // Enable transport arrow
        if (transport) transport.EnableTransport();
        
        // All bolts removed
        ventOpened = true;

        ventGrid.isKinematic = false;

        // EXIT vent interaction mode
        ExitVent();

        // DISABLE THIS SCRIPT SO IT CAN'T STEAL E INPUT
        // enabled = false;

        // Enable transport
        if (transport)
            transport.EnableTransport();
        
    }


    public void Release() { }
    
    void OpenVent()
    {
        ventGrid.isKinematic = false;

        transport.EnableTransport();
    }

}
