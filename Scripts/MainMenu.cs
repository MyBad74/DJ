using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load your game scene; replace "GameScene" with the actual name of your game scene
        SceneManager.LoadScene("Cinematic");
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quit!"); // This line is for testing in the editor
        Application.Quit();
    }
}
