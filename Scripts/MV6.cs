using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//tmp use
using TMPro;

public class MV6 : MonoBehaviour
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

    // Assign the SpriteRenderer of the blood object in the inspector
    public SpriteRenderer bloodSpriteRenderer;

    public GameObject emoji; // Assign the emoji object in the inspector
    public GameObject image1; // Assign the image1 object in the inspector
    public GameObject image2; // Assign the image2 object in the inspector
    public GameObject image3; // Assign the image3 object in the inspector

    public AudioSource footstepsAudioSource; // Assign in inspector

    public Animator bloodAnimator; // Assign the Animator of the blood object in the inspector

    // Camera and zoom-related parameters
    public Camera mainCamera; // Assign the main camera in the inspector
    public float zoomInDuration = 1f; // Duration of the zoom-in effect
    public float zoomedInDuration = 1f; // Duration to stay zoomed-in
    public float zoomOutDuration = 1f; // Duration of the zoom-out effect
    public float targetZoomSize = 2f; // Target zoom size for orthographic camera
    public Vector3 zoomFocusPoint; // Point to focus on when zooming in
    public CF3 cameraFollowScript; // Reference to the CF3 script

    private Vector3 originalCameraPosition;
    private float originalCameraSize;
    private bool hasStoppedOnce = false; // To ensure coroutine triggers only once

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

        // Ensure the blood sprite renderer is assigned in the inspector
        if (bloodSpriteRenderer == null)
        {
            Debug.LogError("Blood Sprite Renderer is not assigned!");
        }
        else
        {
            // The blood sprite renderer should start as invisible
            bloodSpriteRenderer.enabled = false;
        }
        if (fadeImage == null)
        {
            Debug.LogError("Fade Image is not assigned!");
        }

        // Set image2 and image3 to inactive
        image2.SetActive(false);
        image3.SetActive(false);

        // Play initial sound
        Smanager.Instance.PlaySound(1); // Play audio 0 from the start
    }

    void Update()
    {
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

            // If the x position of the player is greater than 12, trigger the "death" trigger and stop the player from moving
            if (transform.position.x >= 12 && !hasStoppedOnce)
            {
                emoji.SetActive(false);
                image1.SetActive(false);
                image2.SetActive(true);
                animator.SetTrigger("Death");
                Smanager.Instance.StopSound(1); // Stop audio 0
                Smanager.Instance.PlaySound(0); // Play audio 2
                moveInput = 0;
                canMove = false;

                // Capture the original camera position and size right before zooming
                originalCameraPosition = mainCamera.transform.position;
                originalCameraSize = mainCamera.orthographicSize;

                hasStoppedOnce = true;

                StartCoroutine(ZoomInCamera());
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
    }

    private void bloodAnimation()
    {
        if (bloodSpriteRenderer != null)
        {
            bloodSpriteRenderer.enabled = true;
        }
        if (bloodAnimator != null)
        {
            bloodAnimator.SetTrigger("death");
        }
    }

    private void turnimageoff()
    {
        image2.SetActive(false);
        image3.SetActive(true);
    }

    IEnumerator PlaySoundFinal()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            Smanager.Instance.PlaySound(2);
            yield return new WaitForSeconds(200);
        }
    }

    // Coroutine to handle camera zoom-in effect
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

    // Coroutine to show credits
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
