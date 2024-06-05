using UnityEngine;
using UnityEngine.SceneManagement; // Add this line

public class EscMenu : MonoBehaviour
{
    public GameObject optionsMenuPanel; // Assign this in the Inspector
    public GameObject settingsPanel; // Assign this in the Inspector
    public ButtonHoverEffectNew[] buttonHoverEffects;

    // Make it hidden when the game starts
    private void Start()
    {
        optionsMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void Update()
    {
        // Check if the Esc key was pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeSelf)
            {
                settingsPanel.SetActive(false);
            }

            // Toggle the options menu visibility
            optionsMenuPanel.SetActive(!optionsMenuPanel.activeSelf);

            // Optionally, pause the game when the options menu is open
            if (optionsMenuPanel.activeSelf)
            {
                Time.timeScale = 0f; // Pause the game
                ResetButtonStates();
            }
            else
            {
                Time.timeScale = 1f; // Resume the game
            }
        }
    }

    public void ToggleOptionsMenu()
    {
        bool isActive = optionsMenuPanel.activeSelf;
        optionsMenuPanel.SetActive(!isActive);

        // Manage game pause state
        Time.timeScale = isActive ? 1f : 0f;

        // Close the settings window when opening the options menu
        if (isActive)
        {
            settingsPanel.SetActive(false);
        }

        // Reset button states whenever the options menu is toggled
        ResetButtonStates();
    }

    public void MainMenuOption()
    {
        Time.timeScale = 1f; // Ensure the game's time scale is reset
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your main menu scene's name
    }

    public void ToggleSettingsWindow()
    {
        // Toggle the settings window visibility
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void UnToggleSettingsWindow()
    {
        // Disable the settings window
        settingsPanel.SetActive(false);
    }

    private void ResetButtonStates()
    {
        foreach (var ButtonHoverEffectNew in buttonHoverEffects)
        {
            ButtonHoverEffectNew.ResetButtonState();
        }
    }
}
