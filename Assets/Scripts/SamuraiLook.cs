using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move the camera in first person perspective

public class SamuraiLook : MonoBehaviour
{

    public Transform cameraPosition;
    public Transform orientation;
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
