using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Required for working with UI elements like Image

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite spriteOne; // Assign in the Inspector
    public Sprite spriteTwo; // Assign in the Inspector

    private Image imageComponent; // To hold the Image component of the GameObject

    public CardManager cardManager; // Ensure this is linked in the Inspector

    private void Start()
    {
        imageComponent = GetComponent<Image>(); // Get the Image component on start
        imageComponent.sprite = spriteOne; // Ensure the image starts with spriteOne
    }

    public void SwitchSprite()
    {
        // Check if it's the final selection phase before flipping
        if (!cardManager.IsFinalSelectionPhase())
        {
            StartCoroutine(FlipCard());
        }
        else
        {
            // In the final selection phase, just notify CardManager without flipping
            if (cardManager != null)
            {
                cardManager.SpriteSwitcherClicked(this);
            }
        }
    }

    private IEnumerator FlipCard()
    {
        // Flip halfway to give the illusion of flipping like a card
        for (float i = 0; i <= 90; i += 10)
        {
            transform.localRotation = Quaternion.Euler(0, i, 0);
            yield return new WaitForSeconds(0.01f);
        }

        // Switch the sprite when the card is edge-on
        imageComponent.sprite = imageComponent.sprite == spriteOne ? spriteTwo : spriteOne;

        // Complete the flip to show the new sprite
        for (float i = 90; i >= 0; i -= 10)
        {
            transform.localRotation = Quaternion.Euler(0, i, 0);
            yield return new WaitForSeconds(0.01f);
        }

        // Notify CardManager after the flip
        cardManager.SpriteSwitcherClicked(this);
    }
}
