using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiSlash : MonoBehaviour
{
    public float slashDamage = 20f; 
    public LayerMask enemy; 

    public AudioClip slashSound;
    private AudioSource audioSource;

    public float yOffset = 20f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PerformSlash();
            Debug.DrawRay(transform.position, transform.forward * 4f, Color.red);
        }
    }

    private void PerformSlash()
    {
        if (audioSource != null && slashSound != null)
        {
            audioSource.PlayOneShot(slashSound);
        }
        // Raycast to detect enemies in front of the player
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + Vector3.up * yOffset;
        if (Physics.Raycast(raycastOrigin, transform.forward, out hit, 4f, enemy))
        {
            // Check if the hit object has an Enemy script (replace with your enemy script)
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                // Deal damage to the enemy
                enemy.TakeDamage(slashDamage);
                Debug.Log("Attacked!");
            }
        }

    }
}
