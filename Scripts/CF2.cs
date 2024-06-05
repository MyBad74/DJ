using UnityEngine;

public class CF2 : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float stopFollowX =  100;

    private bool shouldFollow = true;

    void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (shouldFollow)
        {
            if (target.position.x >= stopFollowX)
            {
                shouldFollow = false;
                return;
            }

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
