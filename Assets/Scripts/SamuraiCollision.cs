using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiCollision : MonoBehaviour
{

    public SamuraiMove movement;
    public SamuraiLook camera;
    public SamuraiSlash slash;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("pit"))
        {
            Debug.Log("Entered pit");
            PlayerInputDisabled();
            FindObjectOfType<GameManager>().GameOver();
            FindObjectOfType<Stopwatch>().stopTimer();

        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("goal"))
        {
            Debug.Log("Reached the goal!");
            PlayerInputDisabled();
            FindObjectOfType<GameManager>().Victory();
            FindObjectOfType<Stopwatch>().stopTimer();
        }
        else if (other.tag == "BasicProjectile")
        {
            Debug.Log("Entered pit");
            PlayerInputDisabled();
            FindObjectOfType<GameManager>().GameOver();
            FindObjectOfType<Stopwatch>().stopTimer();
        }
    }

    public void PlayerInputDisabled()
    {
        camera.enabled = false;     // Disable camera controls
        movement.enabled = false;   // Disable player movement w
        slash.enabled = false;      // Disable attack
    }

    public void PlayerInputEnabled()
    {
        camera.enabled = true;     // Enable camera controls
        movement.enabled = true;   // Enable player movement 
        slash.enabled = true;
    }
    
}
