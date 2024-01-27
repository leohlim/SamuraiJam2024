using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move the camera in first person perspective

public class SamuraiLook : MonoBehaviour
{
    // Controls the first person player camera. Note: the Main Camera should never be a child of the Player prefab. Doing so
    // will cause camera jittering with the Player rigidbody.

    public Transform cameraPosition;            // Link the CameraPos game object under the Player Prefab
    public Transform orientation;               // Link the Orientation game object under the Player Prefab
    public float mouseSensitivity = 100f;

    float xRotation;
    float yRotation;

    private void Awake()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = cameraPosition.position;
    }

    private void LateUpdate()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity ;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity ;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
