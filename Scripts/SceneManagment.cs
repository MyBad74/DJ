using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneManagment : MonoBehaviour
{

    public void ChangeScene(string sceneName)
    {
        //if current scene aname is 7th scene or 8th scene, sopt instance of sound manager
        if (SceneManager.GetActiveScene().name == "7th Scene")
        {
            Smanager.Instance.StopSound(2);
        }
        else if (SceneManager.GetActiveScene().name == "8th Scene")
        {
            Smanager.Instance.StopSound(1);
        }
        SceneManager.LoadScene(sceneName);
    }

}
