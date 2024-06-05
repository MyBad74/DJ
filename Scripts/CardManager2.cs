using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for loading scenes
using TMPro;

public class CardManager2 : MonoBehaviour
{
    private List<Sprite2> clickedSwitchers = new List<Sprite2>();
    public List<GameObject> allSwitcherObjects; // Assign all GameObjects with SpriteSwitcher components in the Inspector
    public GameObject loadSceneButton; // Assign this button in the inspector
    private Sprite2 finalSelectedSwitcher; // To store the final selected switcher
    private Dictionary<Sprite2, string> switcherToSceneMap; // Map each switcher to a scene

    //reference to a text(tmp) object
    public GameObject subtitleText;
    public GameObject subtitleText2;

    private bool isFinalSelectionPhase = false; // To track if we are in the final selection phase

    void Start()
    {
        InitializeSwitcherSceneMap();
        //set subtitle text to false
        subtitleText2.SetActive(false);

        loadSceneButton.SetActive(false); // Hide button initially
    }

    public void SpriteSwitcherClicked(Sprite2 switcher)
    {

        if (!clickedSwitchers.Contains(switcher) && !isFinalSelectionPhase)
        {
            Smanager.Instance.PlaySound(0);

            clickedSwitchers.Add(switcher);
            if (clickedSwitchers.Count == 3)
            {
                EnableCard();
                HideUnselectedSwitchers();
                subtitleText.SetActive(false);
                subtitleText2.SetActive(true);
                isFinalSelectionPhase = true;
            }

        }
        else if (isFinalSelectionPhase)
        {
            if (switcher != null && !clickedSwitchers.Contains(switcher))
                return;
            finalSelectedSwitcher = switcher; // Store final switcher for later
            subtitleText2.SetActive(false);
            FinalizeSelection(switcher);
            TriggerLastCardAnimation(switcher.gameObject); // Play any final animations
            //ShowLoadSceneButton(); // Show the button to load the scene
        }
    }

    private void ShowLoadSceneButton()
    {
        loadSceneButton.SetActive(true);
    }

    private void InitializeSwitcherSceneMap()
    {
        switcherToSceneMap = new Dictionary<Sprite2, string>();
        foreach (var switcherObject in allSwitcherObjects)
        {
            var switcher = switcherObject.GetComponent<Sprite2>();
            if (switcher != null)
            {
                // Example mapping based on GameObject name
                switch (switcher.gameObject.name)
                {
                    case "Card1":
                        switcherToSceneMap[switcher] = "6th Scene";
                        break;
                    case "Card2":
                        switcherToSceneMap[switcher] = "6th Scene";
                        break;
                    case "Card3":
                        switcherToSceneMap[switcher] = "6th Scene";
                        break;
                    case "Card4":
                        switcherToSceneMap[switcher] = "6th Scene 2";
                        break;
                    case "Card5":
                        switcherToSceneMap[switcher] = "6th Scene 2";
                        break;
                    case "Card6":
                        switcherToSceneMap[switcher] = "6th Scene 2";
                        break;
                }
            }
            else
            {
                Debug.LogError("Switcher component not found on " + switcherObject.name);
            }
        }
    }

    public void LoadFinalScene()
    {
        if (
            finalSelectedSwitcher != null
            && switcherToSceneMap.TryGetValue(finalSelectedSwitcher, out string sceneName)
        )
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void LoadSceneForSwitcher(Sprite2 switcher)
    {
        if (switcherToSceneMap.TryGetValue(switcher, out string sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("No scene mapped for this switcher: " + switcher.gameObject.name);
        }
    }

    public bool IsFinalSelectionPhase()
    {
        return isFinalSelectionPhase;
    }

    void DisableCard()
    {
        foreach (var switcherObject in clickedSwitchers)
        {
            var collider = switcherObject.GetComponent<Collider2D>();
            if (collider != null)
                collider.enabled = false; // Correctly disable collider to make it unclickable
        }
    }

    void EnableCard()
    {
        foreach (var switcherObject in clickedSwitchers)
        {
            if (!clickedSwitchers.Contains(switcherObject))
            {
                var collider = switcherObject.GetComponent<Collider2D>();
                if (collider != null)
                    collider.enabled = true; // Enable collider to make it clickable
            }
        }
    }

    void HideUnselectedSwitchers()
    {
        foreach (var switcherObject in allSwitcherObjects)
        {
            Sprite2 switcherComponent = switcherObject.GetComponent<Sprite2>();
            if (switcherComponent != null && !clickedSwitchers.Contains(switcherComponent))
            {
                // Disable interaction here
                var collider = switcherObject.GetComponent<Collider2D>();
                if (collider != null)
                    collider.enabled = false; // Disable collider to make it unclickable

                TriggerHideAnimation(switcherObject);
            }
        }
    }

    void FinalizeSelection(Sprite2 selectedSwitcher)
    {
        foreach (var switcherObject in allSwitcherObjects)
        {
            Sprite2 switcherComponent = switcherObject.GetComponent<Sprite2>();
            if (switcherComponent != null && switcherComponent != selectedSwitcher)
            {
                TriggerHideAnimation(switcherObject);
            }
        }
        // Optionally reset or perform additional actions here
    }

    void TriggerHideAnimation(GameObject switcherObject)
    {
        Animator animator = switcherObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("TriggerCardOut");
        }
        else
        {
            switcherObject.SetActive(false); // Fallback if no Animator is found
        }
    }

    void TriggerLastCardAnimation(GameObject switcherObject)
    {
        Animator animator = switcherObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Middle");
        }
        else
        {
            switcherObject.SetActive(false); // Fallback if no Animator is found
        }
    }
}
