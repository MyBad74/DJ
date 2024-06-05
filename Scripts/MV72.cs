using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MV72 : MonoBehaviour
{
    public Transform groundCheck; // Assign in inspector
    public float groundCheckRadius = 0.2f; // Radius of the ground check
    public LayerMask groundLayer; // Assign in inspector
    public float jumpForce = 10f; // Adjust as needed
    public float moveSpeed = 5f; // Speed of horizontal movements

    public Camera mainCamera; // Assign the main camera in the inspector
    public float zoomInDuration = 1f; // Duration of the zoom-in effect
    public float zoomedInTime = 20f; // Duration to stay zoomed in
    public float zoomOutDuration = 1f; // Duration of the zoom-out effect
    public float targetZoomSize = 2f; // Target zoom size for orthographic camera
    public Vector3 zoomFocusPoint; // Point to focus on when zooming in

    public List<MonoBehaviour> parallaxEffectScripts; // List of parallax effect scripts
    public CF3 cameraFollowScript; // Reference to the CF3 script

    private Rigidbody2D rb;
    private Animator animator; // Add reference to the Animator
    private bool isGrounded;
    private float moveInput; // To store horizontal movement input
    private bool facingRight = true; // To keep track of the character's facing direction
    private bool isStopped = false; // To track if the character should be stopped
    private bool hasStoppedOnce = false; // To ensure coroutine triggers only once
    private Vector3 originalCameraPosition;
    private float originalCameraSize;
    public AudioSource footstepsAudioSource; // Assign in inspector

    public float triggerXPosition = 10; // X position to trigger the other object's animation
    public Animator otherAnimator; // Assign the Animator of the other object in the inspector

    public SubManager2 subtitleManager; // Reference to the SubtitleManager

    public FadeManager fadeManager;
    public float fadeStartX = 25f;

    public GameObject image1;
    public GameObject Image2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject

        if (cameraFollowScript == null)
        {
            cameraFollowScript = mainCamera.GetComponent<CF3>();
        }

        //image 2 invisible
        Image2.SetActive(false);
    }

    void Update()
    {
        // Check if the character is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Get horizontal movement input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Stop movement if the character reaches x = 29 and the coroutine hasn't been triggered before
        if (transform.position.x >= triggerXPosition && !hasStoppedOnce)
        {
            // Capture the original camera position and size right before zooming
            originalCameraPosition = mainCamera.transform.position;
            originalCameraSize = mainCamera.orthographicSize;

            // Trigger the other object's animation
            otherAnimator.SetTrigger("falar");
            // Start subtitles
            subtitleManager.StartSubtitles();

            hasStoppedOnce = true;

            if (footstepsAudioSource.isPlaying)
            {
                footstepsAudioSource.Stop();
            }
            StartCoroutine(ZoomInCamera());
        }

        // Allow movement only if not stopped
        if (!isStopped)
        {
            animator.SetBool("isRunning", Mathf.Abs(moveInput) > 0);

            // Play or stop the footsteps sound based on movement
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
        }
        else
        {
            // Stop running animation when stopped
            animator.SetBool("isRunning", false);
        }

        if (transform.position.x >= fadeStartX)
        {
            fadeManager.StartFadeOut();

            //force ines to keep moving to the right
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement in FixedUpdate for physics consistency
        if (!isStopped)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
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

    // Coroutine to handle camera zoom-in effect
    IEnumerator ZoomInCamera()
    {
        isStopped = true;

        // Disable the camera follow script
        if (cameraFollowScript != null)
        {
            cameraFollowScript.enabled = false;
        }

        float startSize = mainCamera.orthographicSize;
        Vector3 startPosition = mainCamera.transform.position;
        float elapsed = 0f;

        // Disable all parallax effects
        foreach (var parallaxEffectScript in parallaxEffectScripts)
        {
            if (parallaxEffectScript != null)
            {
                parallaxEffectScript.enabled = false;
            }
        }

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

        Image2.SetActive(true);
        image1.SetActive(false);
        yield return new WaitForSeconds(zoomedInTime);

        StartCoroutine(ZoomOutCamera());
    }

    // Coroutine to handle camera zoom-out effect
    IEnumerator ZoomOutCamera()
    {
        float startSize = mainCamera.orthographicSize; // Start from the current zoomed-in size
        Vector3 startPosition = mainCamera.transform.position; // Start from the current zoomed-in position
        float elapsed = 0f;

        //image 2 active and image 1 inactive

        while (elapsed < zoomOutDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(
                startSize,
                originalCameraSize,
                elapsed / zoomOutDuration
            );
            mainCamera.transform.position = Vector3.Lerp(
                startPosition,
                originalCameraPosition,
                elapsed / zoomOutDuration
            );
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.orthographicSize = originalCameraSize;
        mainCamera.transform.position = originalCameraPosition;

        // Re-enable all parallax effects
        foreach (var parallaxEffectScript in parallaxEffectScripts)
        {
            if (parallaxEffectScript != null)
            {
                parallaxEffectScript.enabled = true;
            }
        }

        // Re-enable the camera follow script
        if (cameraFollowScript != null)
        {
            cameraFollowScript.enabled = true;
        }

        isStopped = false;
    }
}
