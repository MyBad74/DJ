using UnityEngine;

public class MV5 : MonoBehaviour
{
    public Transform groundCheck; // Assign in inspector
    public float groundCheckRadius = 0.2f; // Radius of the ground check
    public LayerMask groundLayer; // Assign in inspector
    public float jumpForce = 10f; // Adjust as needed
    public float moveSpeed = 5f; // Speed of horizontal movement
    public float triggerXPosition = 10; // X position to trigger the other object's animation
    public Animator otherAnimator; // Assign the Animator of the other object in the inspector
    //transform of the other object
    public Transform otherTransform;

    private Rigidbody2D rb;
    private Animator animator; // Add reference to the Animator
    private bool isGrounded;
    private float moveInput; // To store horizontal movement input
    private bool facingRight = true; // To keep track of the character's facing direction
    private bool triggerActivated = false; // To ensure the trigger is activated only once
    private bool canMove = true; // To control movement

    public AudioSource footstepsAudioSource; // Assign in inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
    }

    void Update()
    {
        if (canMove)
        {
            // Check if the character is on the ground
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            // Get horizontal movement input
            moveInput = Input.GetAxisRaw("Horizontal");

            // Allow movement
            animator.SetBool("isRunning", Mathf.Abs(moveInput) > 0);

            if (Mathf.Abs(moveInput) > 0)
            {
                if (!footstepsAudioSource.isPlaying)
                {
                    footstepsAudioSource.Play();
                }
            }
            else
            {
                if (footstepsAudioSource.isPlaying)
                {
                    footstepsAudioSource.Stop();
                }
            }

            // Flip the character if moving in the opposite direction
            if (moveInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveInput < 0 && facingRight)
            {
                Flip();
            }

            // Check if the character has reached the trigger position
            if (!triggerActivated && transform.position.x >= triggerXPosition)
            {

                //if the other transform is in x>30, the trigger is "Trigger2" if the other transform is in x<30, the trigger is "Trigger"
                triggerActivated = true;
                
                if (otherTransform.position.x > 30)
                {
                    otherAnimator.SetTrigger("Trigger2");
                }
                else
                {
                    otherAnimator.SetTrigger("Trigger");
                }
                canMove = false; // Disable movement
                rb.velocity = Vector2.zero; // Stop any existing movement
            }
        }
        else
        {
            // If movement is disabled, ensure the character stops moving
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("isRunning", false);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            // Apply horizontal movement in FixedUpdate for physics consistency
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }

    // Flip the character to face the direction of movement
    void Flip()
    {
        facingRight = !facingRight; // Toggle the value of facingRight
        Vector3 scale = transform.localScale; // Get the local scale
        scale.x *= -1; // Multiply the x component of local scale by -1
        transform.localScale = scale; // Assign the local scale back to the transform
    }

    // Visualize the ground check in the scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
