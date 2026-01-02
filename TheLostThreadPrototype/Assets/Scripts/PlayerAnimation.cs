using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private bool wasGrounded;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        wasGrounded = true;
    }

    private void Update()
    {
        // Horizontal speed for run animation
        float speed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude;
        animator.SetFloat("Speed", speed);

        // Jump / Fall
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.2f);

        // JumpUp: leaving ground
        animator.SetBool("IsJumping", !isGrounded && rb.linearVelocity.y > 0.1f);

        // JumpDown: falling
        animator.SetBool("IsFalling", rb.linearVelocity.y < -0.1f);

        wasGrounded = isGrounded;
    }
}