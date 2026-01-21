using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeypadButton : MonoBehaviour
{
    [Header("Button")]
    public string digit;
    public KeypadInteractable keypad;

    [Header("Press Animation")]
    public float pressDepth = 0.02f;
    public float pressSpeed = 10f;

    private Vector3 startPos;
    private bool isPressed;

    private Camera cam;
    private Collider col;

    private void Awake()
    {
        startPos = transform.localPosition;
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        if (cam == null)
            cam = Camera.main;

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            KeypadButton button = hit.collider.GetComponent<KeypadButton>();
            if (button != null)
            {
                button.Press();
            }
        }
    }


    public void Press()
    {
        Debug.Log("Button clicked: " + digit);

        isPressed = true;
        StartCoroutine(PressAnim());
        keypad.PressButton(digit);
    }

    IEnumerator PressAnim()
    {
        Vector3 pressedPos = startPos - transform.up * pressDepth;

        while (Vector3.Distance(transform.localPosition, pressedPos) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                pressedPos,
                Time.deltaTime * pressSpeed
            );
            yield return null;
        }

        while (Vector3.Distance(transform.localPosition, startPos) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                startPos,
                Time.deltaTime * pressSpeed
            );
            yield return null;
        }

        transform.localPosition = startPos;
        isPressed = false;
    }
}
