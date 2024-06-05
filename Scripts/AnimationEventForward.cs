using UnityEngine;

public class AnimationEventForwarder : MonoBehaviour
{
    public GameObject targetObject; // Assign GameManager here in the inspector

    public void ForwardAnimationComplete()
    {
        targetObject.GetComponent<LetterButton>().OnAnimationComplete();
    }
}
