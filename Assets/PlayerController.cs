using UnityEngine;

public class WaterPlayerController : MonoBehaviour
{
    // Movement Variables
    public float moveSpeed = 5f;           // Movement speed
    public float rotationSpeed = 5f;      // Speed at which the frog "turns" to face the camera's direction
    public float verticalSpeed = 3f;      // Up/down vertical movement speed

    // References
    public Transform cameraTransform;     // Reference to the camera's transform
    public Transform lookAtTransform;     // The point the camera is looking at (e.g., a target)

    private Rigidbody rb;
    public Animator animator;

    void Start()
    {
        // Get Rigidbody
        rb = GetComponent<Rigidbody>();

        // Disable gravity and add drag to simulate water-like movement
        if (rb)
        {
            rb.useGravity = false;   // Disable gravity for underwater movement
            rb.linearDamping = 2f;            // Add drag to simulate water resistance
            rb.angularDamping = 4f;     // Smooth out rotations
        }

        // Find the camera if it's not assigned
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        // Handle movement and rotation
        // HandleRotation();
        HandleMovement();
        HandleVerticalMovement();
    }

    // Rotate the frog to face the outward direction of the camera (camera's forward direction)
    Vector3 HandleRotation()
    {
        // if (cameraTransform == null || lookAtTransform == null)
        // {
        //     Debug.LogWarning("Make sure both cameraTransform and lookAtTransform are assigned in the Inspector!");
        //     return;
        // }

        // Calculate the direction from the camera to the LookAt point
        Vector3 lookDirection = lookAtTransform.position - cameraTransform.position;
        Debug.DrawLine(cameraTransform.position, lookAtTransform.position, Color.red);

        // Ignore vertical (Y-axis) component to keep the rotation aligned to the X-Z plane
        // lookDirection.y = 0;

        // Ensure there's enough direction vector to rotate towards
        // if (lookDirection.sqrMagnitude > 0.1f)
        // {
            // Debug.Log("ROTATING");
            // Get the rotation needed to face the calculated direction
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            // Smoothly rotate the frog toward the calculated direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        // }
        return lookDirection;
    }

    // Move the frog forward/backward based on input
    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down arrow keys
        // float mouseY = Input.GetAxis("Mouse Y");
        
        if (horizontal == 0 && vertical == 0)
        {
            return;
        }


        animator.SetTrigger("Swim");
        // var rotationQuaternion = 
        var camDirection = HandleRotation();

        // Vector3 forwardMovement = transform.forward * vertical * moveSpeed;  // Forward/backward
        Vector3 strafeMovement = transform.right * horizontal * moveSpeed;   // Left/right
        Vector3 verticalMovement = camDirection.normalized * moveSpeed;        // Up/down
        // Debug.Log("Horizontal: " + horizontal + " Vertical: " + vertical );
        // Debug.Log("Cam Direction: " + camDirection );

        Vector3 target = verticalMovement;
        Vector3 velocityChange = target - rb.linearVelocity;
        Vector3 force = Vector3.ClampMagnitude(velocityChange + strafeMovement, 20);
        rb.AddForce(force, ForceMode.VelocityChange);
    }

    // Allow vertical movement (up/down) with Q/E keys
    void HandleVerticalMovement()
    {
        float verticalMovement = 0f;

        if (Input.GetKey(KeyCode.E)) // Move up
        {
            verticalMovement = verticalSpeed;
        }
        else if (Input.GetKey(KeyCode.Q)) // Move down
        {
            verticalMovement = -verticalSpeed;
        }

        // Add vertical velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, verticalMovement, rb.linearVelocity.z);
    }
}