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

    public Canvas playerHUD;

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

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        animator.SetTrigger("Swim");
    }

    void FixedUpdate()
    {
        HandleMovement();
        // HandleVerticalMovement();
    }


    // Rotate the frog to face the outward direction of the camera (camera's forward direction)
    Vector3 HandleRotation()
    {
        Vector3 lookDirection = lookAtTransform.position - cameraTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        
        return lookDirection;
    }

    // Move the frog forward/backward based on input
    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down arrow keys
        
        if (horizontal == 0 && vertical == 0)
        {
            return;
        }

        bool canSwim = !animator.IsInTransition(0) && 
            (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PBR Frog_Anim_Idle" || animator.GetCurrentAnimatorClipInfo(0)[0].clip.name ==  "PBR Frog_Anim_Idle_Swim");


        if (canSwim) {
           animator.SetTrigger("Swim");
        }

        var camDirection = HandleRotation();

        Vector3 strafeMovement = transform.right * horizontal * moveSpeed;   // Left/right
        Vector3 verticalMovement = camDirection.normalized * moveSpeed;        // Up/down

        Vector3 target = verticalMovement;
        Vector3 velocityChange = target - rb.linearVelocity;
        if (velocityChange.sqrMagnitude > 0.5f) // Threshold; adjust as needed
        {
            Vector3 force = Vector3.ClampMagnitude(velocityChange + strafeMovement, 500);
            rb.AddForce(force, ForceMode.VelocityChange);
        }
    }

    // // Allow vertical movement (up/down) with Q/E keys
    // void HandleVerticalMovement()
    // {
    //     float verticalMovement = 0f;

    //     if (Input.GetKey(KeyCode.E)) // Move up
    //     {
    //         verticalMovement = verticalSpeed;
    //     }
    //     else if (Input.GetKey(KeyCode.Q)) // Move down
    //     {
    //         verticalMovement = -verticalSpeed;
    //     }

    //     // Add vertical velocity
    //     rb.linearVelocity = new Vector3(rb.linearVelocity.x, verticalMovement, rb.linearVelocity.z);
    // }
}