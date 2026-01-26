using UnityEngine;

public class MagnetTarget : MonoBehaviour
{
    private Rigidbody rb;
    private MagnetForce magnetForce;

    private Vector3 pullDirection;
    private bool isStuck;

    [Header("Stick Settings")]
    public float stickDistance = 0.35f;
    public float stickSpeed = 25f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (magnetForce == null) return;
        if (!magnetForce.IsActive)
        {
            magnetForce = null;
            isStuck = false;
            return;
        }

        Vector3 toMagnet = magnetForce.transform.position - transform.position;
        float distance = toMagnet.magnitude;

        // Stick when close
        if (!isStuck && distance <= stickDistance)
        {
            isStuck = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (isStuck)
        {
            Vector3 targetPos = magnetForce.transform.position;
            rb.MovePosition(Vector3.Lerp(rb.position, targetPos, Time.fixedDeltaTime * stickSpeed));
            rb.angularVelocity = Vector3.zero;
            return;
        }

        // Pull force when far
        pullDirection = toMagnet.normalized;
        rb.AddForce(pullDirection * magnetForce.ForceStrength, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ONLY react if the OTHER collider is a Magnet
        if (!other.TryGetComponent<MagnetForce>(out var magnet)) return;

        magnetForce = magnet;
        Debug.Log($"{name} found a magnet");
    }

    private void OnTriggerExit(Collider other)
    {
        if (magnetForce == null) return;

        if (other.TryGetComponent<MagnetForce>(out var magnet) && magnet == magnetForce)
        {
            magnetForce = null;
            isStuck = false;
            Debug.Log($"{name} left magnet");
        }
    }
}