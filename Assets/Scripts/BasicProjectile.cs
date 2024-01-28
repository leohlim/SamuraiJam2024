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

    [HideInInspector]
    public Vector3 initialPlayerPosition;

    [HideInInspector]
    public bool isChaser = true; // Flag to differentiate between 'chaser' and 'simple'

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        initialPlayerPosition = Camera.main.transform.position;

        StartCoroutine(DestroyAfterSeconds(projectileLifetime));
    }

    private void Update()
    {
        float step = moveSpeed * Time.deltaTime;

        if (isChaser)
        {
            // Move the projectile towards the player using Lerp
            transform.position = Vector3.Lerp(transform.position, initialPlayerPosition, step);
        }
        else
        {
            // Move the projectile towards the spawner using Lerp
            transform.position = Vector3.Lerp(transform.position, spawnerPosition.position, step);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "ProjectileSpawner":
                other.GetComponent<ProjectileSpawner>().DeathMethod();
                Destroy(gameObject);
                break;
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

