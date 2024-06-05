using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this line to use SceneManager

public class CarController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public Animator[] wheelAnimators;
    public FadeManager fadeManager;
    public float fadeStartX = 25f;

    public AudioSource footstepsAudioSource; // Assign in the Inspector
    public string SceneName = ""; // Corrected 'String' to 'string'

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //if scene name is not set, set it to the current scene
        if (SceneName == "")
        {
            SceneName = SceneManager.GetActiveScene().name;
        }
        StartCoroutine(PlaySound()); // Start the coroutine in the Start method
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (moveInput != 0)
        {
            if (!footstepsAudioSource.isPlaying)
                footstepsAudioSource.Play();
        }
        else
        {
            footstepsAudioSource.Stop();
        }

        // Update the car and wheel animations based on movement
        foreach (Animator anim in wheelAnimators)
        {
            anim.SetFloat("moveInput", moveInput); // Directly using the moveInput value for the Blend Tree
            anim.SetBool("isMoving", moveInput != 0); // Directly using the moveInput value for the condition
        }

        // Trigger fade out when car passes the fadeStartX position
        if (transform.position.x >= fadeStartX)
        {
            fadeManager.StartFadeOut();

            //force the car to keep moving to the right
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    //create routine to play a sound every 2 seconds
    IEnumerator PlaySound()
    {
        if (SceneName == "2nd Scene")
        {
            while (true)
            {
                Smanager.Instance.PlaySound(0);

                yield return new WaitForSeconds(4);
            }
        }
        if (SceneName == "2nd Scene 2")
        {

            yield return new WaitForSeconds(2);
            Smanager.Instance.PlaySound(0);

        }
    }
}
