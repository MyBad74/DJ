using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CF3 : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float maxX; // Maximum x value before camera stops following
    public float minX; // Minimum x value before camera stops following

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
    // Check if the target is within the bounds to follow.
    shouldFollow = target.position.x <= maxX && target.position.x >= minX;

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
