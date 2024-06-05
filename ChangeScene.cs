using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Name of the scene to load
    public string sceneToLoad;

    void OnMouseDown()
    {
        // Change to the specified scene when the object is clicked
        SceneManager.LoadScene(sceneToLoad);
    }
}
