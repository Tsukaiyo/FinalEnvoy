using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float smoothing = 0.3f;
    public float cameraDistance = 3f;
    public LayerMask obstacleMask;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;
    private Renderer lastHitRenderer; // Store the last hit object to re-enable

    void Start()
    {
        transform.rotation = Quaternion.Euler(36.36f, -45f, 0f);
        cam = GetComponent<Camera>() ?? Camera.main;
    }

    void Update()
    { 
        Vector3 desiredPosition = new Vector3(target.position.x + cameraDistance, target.position.y + cameraDistance, target.position.z - cameraDistance);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothing);
    }
}
