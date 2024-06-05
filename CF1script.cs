using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CF1script : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float minX = -1.15f; // Minimum x value before camera stops following

    private bool shouldFollow = true; // State to determine if the camera should follow

    void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void FixedUpdate()
    {
        if (target.position.x < minX)
        {
            // When target is beyond minX, stop following
            shouldFollow = false;
        }
        else if (target.position.x >= minX)
        {
            // Only resume following if target moves back past minX
            shouldFollow = true;
        }

        if (shouldFollow)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(
                smoothedPosition.x,
                transform.position.y,
                transform.position.z
            );
        }
    }
}
