using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill : MonoBehaviour
{
    public Animator ines; // Assign in Inspector
    public Transform inesTransform; // Assign in Inspector
    public GameObject blood;


    // Reference to the MV8 script
    public MV8 mv8; // Assign in Inspector

    // Game objects for images
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;

    void Start()
    {
        if (ines == null)
        {
            Debug.LogError("ines is not assigned!");
        }

        if (mv8 == null)
        {
            Debug.LogError("MV8 script is not assigned!");
        }

        // Initially set image4 inactive
        image4.SetActive(false);
        // Initially set blood inactive
        blood.SetActive(false);
    }

    public void matar()
    {
        if (ines != null)
        {
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(false);
            image4.SetActive(true);

            // Play a different animation based on the flip state
            if (mv8.isFlipped)
            {
                Debug.Log("Flip animation");
                ines.SetTrigger("dieflip");
            }
            else
            {
                Debug.Log("Default animation");
                ines.SetTrigger("die");
                blood.SetActive(true);

            }

            // Activate the blood GameObject and set its position in the next frame to ensure it moves correctly
            StartCoroutine(ActivateAndMoveBlood());

            // Move the inesTransform
            StartCoroutine(MoveInes());
        }
    }

    IEnumerator ActivateAndMoveBlood()
    {
        // Wait for the end of the frame to ensure all positions are updated correctly
        yield return new WaitForEndOfFrame();
        
        blood.SetActive(true);

        // Set the blood position to the hair position

    }

    IEnumerator MoveInes()
    {
        for (int i = 0; i < 200; i++)
        {
            inesTransform.position = new Vector3(
                inesTransform.position.x + 0.003f,
                inesTransform.position.y - 0.003f,
                inesTransform.position.z
            );

            yield return null;
        }
    }

}
