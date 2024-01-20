using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move the camera in first person perspective

public class SamuraiLook : MonoBehaviour
{
    [SerializeField] Transform playerObject;

    public float mouseSensitivity = 100f;

    float xRotation;
    float yRotation;

    private void Start()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 50 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 50 * Time.deltaTime;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerObject.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
