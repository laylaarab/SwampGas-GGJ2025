using UnityEngine;

// Movement script for the frog
public class Movement : MonoBehaviour
{
    public float speed = 1.0f;
    public Rigidbody rb;
    public Animator animator;

    [SerializeField]
    private double JUMP_ANIM_START = 0.2;
    [SerializeField]
    private double JUMP_ANIM_END = 0.9;

    void Start()
    {
        animator.SetTrigger("Idle");
    }

    void FixedUpdate()
    {
        // Spring and damper for the frog to keep it upright
        float springStrength = 1;
        float damperStrength = 1;
        var springTorque = springStrength * Vector3.Cross(rb.transform.up, Vector3.up);
        var dampTorque = damperStrength * -rb.angularVelocity;
        rb.AddTorque(springTorque + dampTorque, ForceMode.Acceleration);
    }

    void OnEnable()
    {
        animator.SetTrigger("Idle");
    }

    void Update()
    {
        // Debug.Log("Current animation " + animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);

        bool canJump = !animator.IsInTransition(0) && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PBR Frog_Anim_Idle";
        if (canJump) {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetTrigger("Jump");
            }
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetTrigger("Jump");
            }
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetTrigger("Jump");
            }
        }
        
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PBR Frog_Anim_Jump")
        {            
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < JUMP_ANIM_START) 
            {
                rb.linearVelocity = new Vector3(0, 0, 0);
            } else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > JUMP_ANIM_END)
            {
                rb.linearVelocity = new Vector3(0, 0, 0);
            } else {
                if (Input.GetKey(KeyCode.W)) {
                    rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
                }
                // Turn frog
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(0, -90 * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(0, 90 * Time.deltaTime, 0);
                }
            }
        }
    }
}
