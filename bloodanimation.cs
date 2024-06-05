using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodAnimation : MonoBehaviour
{
    public SpriteRenderer bloodSpriteRenderer2; // Assign in Inspector
    public Animator bloodAnimator2; // Assign in Inspector

    void Start()
    {
        // Ensure the blood sprite renderer is assigned
        if (bloodSpriteRenderer2 == null)
        {
            Debug.LogError("Blood Sprite Renderer 2 is not assigned!");
        }
        
        // Ensure the blood animator is assigned
        if (bloodAnimator2 == null)
        {
            Debug.LogError("Blood Animator 2 is not assigned!");
        }
    }
    
    public void Play2ndBloodAnimation()
    {
        if (bloodAnimator2 != null)
        {
            bloodAnimator2.SetTrigger("death");
        }
    }
}
