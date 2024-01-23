using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float smoothTime = 1f;

    public float projectileLifetime = 3f;

    private Transform player;

    [HideInInspector]
    public Transform spawnerPosition = null;

    public Vector3 initialDirection;
    private bool isChaser = true; // Flag to differentiate between 'chaser' and 'simple'

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Store the initial direction
        initialDirection = transform.forward;

        StartCoroutine(DestroyAfterSeconds(projectileLifetime));
    }

    private void Update()
    {
        if (isChaser)
        {
            // Move the projectile towards the player using Lerp
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Move the projectile in the direction of its forward vector
            transform.position = Vector3.Lerp(transform.position, spawnerPosition.position, moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator DestroyAfterSeconds(float projectileLifetime)
    {
        yield return new WaitForSeconds(projectileLifetime);

        Destroy(gameObject);
    }

    // Function to switch the projectile type to 'simple'
    public void SwitchToSimple()
    {
        isChaser = false;
    }
}

