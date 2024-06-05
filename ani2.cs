using UnityEngine;

public class ani2 : MonoBehaviour
{
    public GameObject targetObject; // Assign GameManager here in the inspector

    public void ForwardAnimationComplete()
    {
        targetObject.GetComponent<LetterButton2>().OnAnimationComplete();
    }
}
