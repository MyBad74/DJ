using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MV3 : MonoBehaviour
{
    public Transform groundCheck; // Assign in inspector
    public float groundCheckRadius = 0.2f; // Radius of the ground check
    public LayerMask groundLayer; // Assign in inspector
    public float jumpForce = 10f; // Adjust as needed
    public float moveSpeed = 5f; // Speed of horizontal movement

    public Camera mainCamera; // Assign the main camera in the inspector
    public float zoomInDuration = 1f; // Duration of the zoom-in effect
    public float zoomedInDuration = 1f; // Duration to stay zoomed-in
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

    private bool displayedSubtitle = false; // To track if the subtitle has been displayed

    private bool soundPlayed = true; // To track if the sound has been played

    // One string subtitle
    public TextMeshProUGUI subtitleText; // Reference to the TextMeshProUGUI component

    public string subtitle = "Com que então esta família não se dá assim tão bem."; // The subtitle to display

    public string subtitle2 =
        "Hmmm, parece que está a haver uma festa ali dentro, devemos espreitar?"; // The subtitle to display

    // Reference to game objects with sprite renderer
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;

    public GameObject Lrei;
    public GameObject Lsanches;

    private bool enabled = false;
    private bool enabled2 = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject

        if (cameraFollowScript == null)
        {
            cameraFollowScript = mainCamera.GetComponent<CF3>();
        }

        //set image 2 and 3 to inactive
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);

        Lrei.SetActive(false);
        Lsanches.SetActive(false);

        // StartCoroutine(PlaySound3());
    }

    void Update()
    {
        // Check if the character is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Get horizontal movement input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Stop movement if the character reaches x = 29 and the coroutine hasn't been triggered before
        if (transform.position.x >= 29 && !hasStoppedOnce)
        {
            // Capture the original camera position and size right before zooming
            originalCameraPosition = mainCamera.transform.position;
            originalCameraSize = mainCamera.orthographicSize;

            hasStoppedOnce = true;

            if (footstepsAudioSource.isPlaying)
            {
                footstepsAudioSource.Stop();
            }
            StartCoroutine(ZoomInCamera());
        }

        // If character reaches x position of 20, set image1 to inactive and image3 and image2 to active
        if (transform.position.x >= 20 && !enabled)
        {
            //start the sound
            StartCoroutine(PlaySound());
            image1.SetActive(false);
            image2.SetActive(true);
            image3.SetActive(true);
            enabled = true;
        }
        // If character reaches x position of 76, image 4 is set to active and image 1 is set to inactive
        if (transform.position.x >= 76)
        {
            StartCoroutine(PlaySound3());
            image1.SetActive(false);
            image4.SetActive(true);
            image3.SetActive(true);
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

            // If x position is greater than 76, display the second subtitle
            if (transform.position.x >= 76)
            {
                if (!displayedSubtitle)
                {
                    DisplaySubtitle2();
                    displayedSubtitle = true;
                }
            }
        }
        else
        {
            // Stop running animation when stopped
            animator.SetBool("isRunning", false);
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

        // Wait for the zoomed-in duration
        DisplaySubtitle();
        StartCoroutine(PlaySound2());

        Lrei.SetActive(true);
        Lsanches.SetActive(true);
        yield return new WaitForSeconds(zoomedInDuration);

        StartCoroutine(ZoomOutCamera());
    }

    // Coroutine to handle camera zoom-out effect
    IEnumerator ZoomOutCamera()
    {

        Lrei.SetActive(false);  
        Lsanches.SetActive(false);
        float startSize = mainCamera.orthographicSize; // Start from the current zoomed-in size
        Vector3 startPosition = mainCamera.transform.position; // Start from the current zoomed-in position
        float elapsed = 0f;

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
        soundPlayed = false;

        // Ensure images are set correctly

        if (!enabled2)
        {
            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);
            enabled2 = true;
        }
    }

    // Create a function to display the subtitle on the subtitleText for 3 seconds, giving it a subtitle as a parameter
    void DisplaySubtitle()
    {
        StartCoroutine(DisplaySubtitleCoroutine());
    }

    IEnumerator DisplaySubtitleCoroutine()
    {
        subtitleText.text = subtitle;
        yield return new WaitForSeconds(6f);
        subtitleText.text = "";
    }

    void DisplaySubtitle2()
    {
        StartCoroutine(DisplaySubtitleCoroutine2());
    }

    IEnumerator DisplaySubtitleCoroutine2()
    {
        subtitleText.text = subtitle2;
        yield return new WaitForSeconds(6f);
        subtitleText.text = "";
    }

    IEnumerator PlaySound(){

        Smanager.Instance.PlaySound(0); 
        yield return new WaitForSeconds(2f);
    }

    IEnumerator PlaySound2()
    {
        yield return new WaitForSeconds(0f);

        //play sound 5 times
        for (int i = 0; i < 5; i++)
        {
            Smanager.Instance.PlaySound(1);
            yield return new WaitForSeconds(1.25f);
        }
    }

    IEnumerator PlaySound3()
    {

        Smanager.Instance.PlaySound(2);
        yield return new WaitForSeconds(1.25f);
        
    }
}
