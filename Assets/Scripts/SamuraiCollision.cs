using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiCollision : MonoBehaviour
{

    public SamuraiMove movement;
    public SamuraiLook camera;
    public SamuraiSlash slash;

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("pit"))
        {
            Debug.Log("Entered pit");
            camera.enabled = false;     // Disable camera controls
            movement.enabled = false;   // Disable player movement while falling
            slash.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
    }
    
}
