using UnityEngine;
using System.Collections;

public class SimpleCharacterMovement : MonoBehaviour
{
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float jumpForce = 10f;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private float moveInput;
    private bool facingRight = true;

    public GameObject birdObject;
    private Animator birdAnimator;

    public AudioSource footstepsAudioSource; // Assign in the Inspector
    public AudioSource specialAudioSource; // Assign in the Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (birdObject != null)
        {
            birdAnimator = birdObject.GetComponent<Animator>();
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        moveInput = Input.GetAxisRaw("Horizontal");

        if (transform.position.x < 20)
        {
            if (moveInput != 0)
            {
                if (!footstepsAudioSource.isPlaying)
                    footstepsAudioSource.Play();
                animator.SetBool("isRunning", true);
                if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight)
                    Flip();
            }
            else
            {
                footstepsAudioSource.Stop();
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            moveInput = 0;
            rb.velocity = Vector2.zero;
            animator.SetBool("isRunning", false);
            footstepsAudioSource.Stop();
            if (birdObject != null)
            {
                birdAnimator.SetTrigger("Trigger");
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
