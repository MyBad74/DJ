using UnityEngine;

public class ParallaxScene2 : MonoBehaviour
{
    private float startPos;
    public GameObject cam;
    public float parallaxEffect;
    private float initialCameraSize;

    void Start()
    {
        startPos = transform.position.x;
        if (cam.TryGetComponent<Camera>(out Camera cameraComponent))
        {
            initialCameraSize = cameraComponent.orthographicSize;
        }
    }

    void Update()
    {
        if (cam.TryGetComponent<Camera>(out Camera cameraComponent))
        {
            // Calculate the distance the camera has moved since the start, then multiply it by the parallax effect
            float dist = (cam.transform.position.x * parallaxEffect);

            // Adjust the parallax effect based on the camera's current orthographic size
            float sizeFactor = cameraComponent.orthographicSize / initialCameraSize;

            // Set the position of the background with the parallax effect applied, adjusted by the size factor
            transform.position = new Vector3(startPos + dist * sizeFactor, transform.position.y, transform.position.z);
        }
    }
}
