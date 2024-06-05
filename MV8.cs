using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MV8 : MonoBehaviour
{
    public Transform groundCheck; // Assign in inspector
    public float groundCheckRadius = 0.2f; // Radius of the ground check
    public LayerMask groundLayer; // Assign in inspector
    public float moveSpeed = 5f; // Speed of horizontal movement

    private Rigidbody2D rb;
    private Animator animator; // Reference to the Animator
    private bool isGrounded;
    private float moveInput; // To store horizontal movement input
    private bool facingRight = true; // To keep track of the character's facing direction
    private bool canMove = true; // To control movement
    private bool knightwalking = true; // To control knight walking

    // Assign the SpriteRenderer of the blood object in the inspector
    // public SpriteRenderer bloodSpriteRenderer;
    public Animator knightAnimator;
    public Transform knightTransform; // Assign in inspector
    public float stopDistance = 1.5f; // Distance to stop

    public AudioSource footstepsAudioSource; // Assign in inspector

    public Animator bloodAnimator; // Assign the Animator of the blood object in the inspector

    // Camera and zoom-related parameters
    public Camera mainCamera; // Assign the main camera in the inspector
    public float zoomInDuration = 1f; // Duration of the zoom-in effect
    public float zoomedInDuration = 1f; // Duration to stay zoomed-in
    public float targetZoomSize = 2f; // Target zoom size for orthographic camera
    public CF3 cameraFollowScript; // Reference to the CF3 script

    private Vector3 originalCameraPosition;
    private float originalCameraSize;
    private bool hasStoppedOnce = false; // To ensure coroutine triggers only once

    public bool isFlipped = false;

    //hair gameobject
    public GameObject hair;

    public Image fadeImage; // Assign in inspector
    public float fadeDuration = 2f; // Duration of the fade effect
    public float targetAlpha = 0.5f; // Final alpha value (0 to 1)

    //one tmp object
    public GameObject text1;
    public GameObject text2;

    public GameObject text3;
    public GameObject button;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (knightAnimator == null)
        {
            Debug.LogError("Knight Animator is not assigned!");
        }

        if (knightTransform == null)
        {
            Debug.LogError("Knight Transform is not assigned!");
        }
        if (fadeImage == null)
        {
            Debug.LogError("Fade Image is not assigned!");
        }

        // Make the knight walk on the x axis
        knightAnimator.SetBool("andar", true);
        Smanager.Instance.PlaySound(0); // Play audio 0 from the start
    }

    void Update()
    {
        // Make the knight walk on the x axis changing his transform position
        if (knightwalking)
        {
            knightTransform.position = new Vector3(
                knightTransform.position.x + 0.003f,
                knightTransform.position.y,
                knightTransform.position.z
            );
        }

        if (canMove)
        {
            // Check if the character is on the ground
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            // Get horizontal movement input
            moveInput = Input.GetAxisRaw("Horizontal");

            // Set the isRunning animation boolean
            animator.SetBool("isRunning", Mathf.Abs(moveInput) > 0);

            // Handle footstep sounds
            HandleFootsteps();

            // Flip the character if moving in the opposite direction
            if (moveInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveInput < 0 && facingRight)
            {
                Flip();
            }

            // Check the distance to the knight
            if (Vector2.Distance(transform.position, knightTransform.position) <= stopDistance)
            {
                StopMovement();
            }
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement in FixedUpdate for physics consistency
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void HandleFootsteps()
    {
        if (Mathf.Abs(moveInput) > 0 && isGrounded)
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
    }

    // Flip the character to face the direction of movement
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFlipped = !isFlipped;

        // Log the flip
        Debug.Log("Flip is " + isFlipped);
    }

    private void StopMovement()
    {
        canMove = false;
        moveInput = 0;
        animator.SetBool("isRunning", false);
        knightAnimator.SetBool("andar", false);

        // Knight stops walking
        knightwalking = false;

        knightAnimator.SetTrigger("kill");
    }

    public void setBloodTrigger()
    {
        StartCoroutine(ZoomInCamera());
        if (bloodAnimator != null)
        {
            bloodAnimator.SetTrigger("blood");
            Smanager.Instance.StopSound(0); // Stop audio 0
        }
    }

    IEnumerator ZoomInCamera()
    {
        StartCoroutine(PlaySoundFinal());

        // Disable the camera follow script
        if (cameraFollowScript != null)
        {
            cameraFollowScript.enabled = false;
        }

        float startSize = mainCamera.orthographicSize;
        Vector3 startPosition = mainCamera.transform.position;

        // Use the current position of Inesbut have z = -20
        Vector3 zoomFocusPoint = new Vector3(transform.position.x, transform.position.y, -20f);
        float elapsed = 0f;

        while (elapsed < zoomInDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(
                startSize,
                targetZoomSize,
                elapsed / zoomInDuration
            );
            mainCamera.transform.position = Vector3.Lerp(
                startPosition,
                zoomFocusPoint,
                elapsed / zoomInDuration
            );
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.orthographicSize = targetZoomSize;
        mainCamera.transform.position = zoomFocusPoint;

        // Wait for the zoomed-in duration
        yield return new WaitForSeconds(zoomedInDuration);

        StartCoroutine(FadeToBlack());



    }


    IEnumerator PlaySoundFinal()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            Smanager.Instance.PlaySound(1);
            yield return new WaitForSeconds(200);
        }
    }

    IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0f, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = targetAlpha;
        fadeImage.color = color;

        StartCoroutine(ShowCredits());
    }

    IEnumerator ShowCredits()
    {

        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Display the credits text

        text1.SetActive(true);

        yield return new WaitForSeconds(2f);

        text1.SetActive(false);

        text2.SetActive(true);

        yield return new WaitForSeconds(2f);

        text2.SetActive(false);

        text3.SetActive(true);

        yield return new WaitForSeconds(2f);

        text3.SetActive(false);

        button.SetActive(true);

    }
}
