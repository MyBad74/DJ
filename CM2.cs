using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CM2 : MonoBehaviour
{
    public GameObject loadSceneButton; // Assign this button in the inspector

    // Start is called before the first frame update
    void Start() { }

    public void ShowButton()
    {
        loadSceneButton.SetActive(true);
    }
}
