using UnityEngine;

// Movement script for the frog
public class Movement : MonoBehaviour
{
    public float speed = 1.0f;
    public Rigidbody rb;
    public Animator animator;
    private bool isJumping = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            isJumping = true;
        }
        if (isJumping && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            animator.SetTrigger("Jump");
            isJumping = false;
        }
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PBR Frog_Anim_Jump")
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.1 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9)
            {
                rb.linearVelocity = new Vector3(0, 0, speed * 4);
            } else 
            {
                rb.linearVelocity = new Vector3(0, 0, 0);
            }
        }
    }
}
