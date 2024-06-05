using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MV4 : MonoBehaviour
{

    public GameObject image;
    public GameObject image2;
    // Start is called before the first frame update
    void Start()
    {

        image.SetActive(false);
        image2.SetActive(false);

        //show images
        StartCoroutine(ShowImages());
        
        //play sound
        StartCoroutine(PlaySound());

    }

    //routine to countinuously play sound
    IEnumerator PlaySound()
    {
        while (true)
        {
            Smanager.Instance.PlaySound(0);
            yield return new WaitForSeconds(1);
        }
    }

    //at 7 seconds, show images
    IEnumerator ShowImages()
    {
        yield return new WaitForSeconds(7);
        image.SetActive(true);
        image2.SetActive(true);
    }

}
