using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    
    [Header("Movement")]
    [SerializeField] private float NormalSpeed = 5f;
    [SerializeField] private float CarryingSpeed = 1.5f;
    [SerializeField] private float DraggingSpeed = 2.5f;
    [SerializeField] private float turnSpeed = 1080f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravityScale = 1f;


    [SerializeField] private PlayerState state;

    private Rigidbody myRigidbody;
    private Vector3 moveDirection;
    
    public Vector3 Direction
    {
        get { return moveDirection; }
    }

    
    private bool isGrounded;

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void OnJump(InputValue value)
    {
        if (!isGrounded) return;

        myRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        // Stop if no input
        if (moveDirection.magnitude <= 0.01f)
        {
            myRigidbody.linearVelocity = new Vector3(0f, myRigidbody.linearVelocity.y, 0f);
            return;
        }

        if (state == PlayerState.Normal)
            NormalState();
    }

    private void NormalState()
    {
        // Movement
        Vector3 velocity = moveDirection.normalized * NormalSpeed;
        velocity.y = myRigidbody.linearVelocity.y;
        myRigidbody.linearVelocity = velocity;

        // Rotation
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        Quaternion smoothRotation = Quaternion.RotateTowards(
            myRigidbody.rotation,
            targetRotation,
            turnSpeed * Time.fixedDeltaTime
        );

        myRigidbody.MoveRotation(smoothRotation);
    }

    private void CarryingState()
    {
        // Movement
        Vector3 velocity = moveDirection.normalized * CarryingSpeed;
        velocity.y = myRigidbody.linearVelocity.y;
        myRigidbody.linearVelocity = velocity;

        // Rotation
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        Quaternion smoothRotation = Quaternion.RotateTowards(
            myRigidbody.rotation,
            targetRotation,
            turnSpeed * Time.fixedDeltaTime
        );

        myRigidbody.MoveRotation(smoothRotation);
    }

    private void DraggingState()
    {
        // Movement
        Vector3 velocity = moveDirection.normalized * DraggingSpeed;
        velocity.y = myRigidbody.linearVelocity.y;
        myRigidbody.linearVelocity = velocity;

        // Rotation
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        Quaternion smoothRotation = Quaternion.RotateTowards(
            myRigidbody.rotation,
            targetRotation,
            turnSpeed * Time.fixedDeltaTime
        );

        myRigidbody.MoveRotation(smoothRotation);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        // Camera-relative directions
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        moveDirection = camForward * input.y + camRight * input.x;
    }
}

