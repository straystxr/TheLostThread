using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class KeypadInteractable : MonoBehaviour, IInteractable
{
    [Header("Camera")]
    public CinemachineCamera keypadCam;
    public CinemachineCamera playerCam;

    [Header("Player")]
    public GameObject player;
    
    [Header("Raycast")]
    public Camera keypadRaycastCamera;
    
    [Header("Keypad")]
    public TMP_Text screenText;
    public string correctCode = "1234";
    public GameObject door;

    [Header("Sounds")]
    public AudioSource buttonSound;
    public AudioSource correctSound;
    public AudioSource wrongSound;

    [Header("Input")]
    public InputActionReference cancelAction; // assign ESC input action here

    [Header("Feedback")]
    public GameObject glowObject;
    public GameObject pressEUI;
    
    private List<string> currentCode = new List<string>();
    private bool isInteracting = false;
    
    [Header("End Game Fade")]
    [SerializeField] public Animator fadeAnimator;
    [SerializeField] private GameObject creditsText;
    [SerializeField] private float creditsDisplayTime = 5f;
    
    

    private void OnEnable()
    {
        if (cancelAction != null)
            cancelAction.action.Enable();
    }

    private void OnDisable()
    {
        if (cancelAction != null)
            cancelAction.action.Disable();
    }

   private void Update()
{
    if (!isInteracting) return;

    // ESC / Cancel
    if (cancelAction != null && cancelAction.action.triggered)
    {
        ExitInteraction();
        return;
    }

    // Mouse click detection
    if (Mouse.current.leftButton.wasPressedThisFrame)
    {
        Ray ray = keypadRaycastCamera.ScreenPointToRay(
            Mouse.current.position.ReadValue()
        );

        int keypadLayer = LayerMask.GetMask("KeypadButton");

        if (Physics.Raycast(ray, out RaycastHit hit, 3f, keypadLayer))
        {
            if (hit.collider.TryGetComponent(out KeypadButton button))
            {
                button.Press();
            }
        }

    }
}

public bool CanHold => false;

public void Interact(Transform interactor)
    {
        if (isInteracting) return;
        isInteracting = true;

        // Hide Glow & UI
        if (glowObject) glowObject.SetActive(false);
        if (pressEUI) pressEUI.SetActive(false);

        // Lock player movement
        var movement = player.GetComponent<PlayerMovement>();
        if (movement) movement.enabled = false;

        // Swap cameras
        if (keypadCam && playerCam)
        {
            keypadCam.Priority = 20;
            playerCam.Priority = 10;
        }

        currentCode.Clear();
        UpdateScreen();

        // Show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void Release()
    {
        ExitInteraction();
    }


    public void ExitInteraction()
    {
        if (!isInteracting) return;
        isInteracting = false;

        // Restore glow & UI
        if (glowObject) glowObject.SetActive(true);
        if (pressEUI) pressEUI.SetActive(true);

        // Unlock player movement
        var movement = player.GetComponent<PlayerMovement>();
        if (movement) movement.enabled = true;

        // Swap cameras back
        if (keypadCam && playerCam)
        {
            keypadCam.Priority = 5;
            playerCam.Priority = 15;
        }

        currentCode.Clear();
        UpdateScreen();

        // Hide cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PressButton(string digit)
    {
        if (!isInteracting) return;

        if (buttonSound) buttonSound.Play();

        currentCode.Add(digit);
        UpdateScreen();

        if (currentCode.Count >= correctCode.Length)
            CheckCode();
    }

    private void UpdateScreen()
    {
        if (screenText)
            screenText.text = currentCode.Count == 0 ? "----" : string.Join("", currentCode);
    }

    private void CheckCode()
    {
        string entered = string.Join("", currentCode);

        if (entered == correctCode)
        {
            if (correctSound) correctSound.Play();
            OpenDoor();
            //Ending game credits sequence
            StartCoroutine(GameEnded());
            return;
        }
        else
        {
            if (wrongSound) wrongSound.Play();
        }

        currentCode.Clear();
        UpdateScreen();
    }

    private void OpenDoor()
    {
        if (!door) return;

        Animation anim = door.GetComponent<Animation>();
        if (anim) anim.Play();

        AudioSource audio = door.GetComponent<AudioSource>();
        if (audio) audio.Play();
    }

    private IEnumerator GameEnded()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayEndingMusic();
        }
        
        //animation trigger for fade in
        fadeAnimator.SetTrigger("fadeInAnim");
        
        //waiting for fade to complete  
        yield return new WaitForSeconds(3f);
        //enable credits text
        creditsText.SetActive(true);
        //wait for credits display time
        yield return new WaitForSeconds(creditsDisplayTime);
        //loading back to main menu
        SceneManager.LoadScene("MainMenu");
    }
}
