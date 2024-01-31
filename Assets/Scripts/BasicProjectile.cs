using System.Collections;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float projectileLifetime = 3f;

    private Transform player;

    [HideInInspector]
    public Transform spawnerPosition = null;

    [HideInInspector]
    public Vector3 directionToPlayer;

    [HideInInspector]
    public bool wasParried = false;

    public bool inParry = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        directionToPlayer = (Camera.main.transform.position - transform.position).normalized;

        StartCoroutine(DestroyAfterSeconds(projectileLifetime));
    }

    private void Update()
    {
        if (wasParried)
        {
            // Calculate the direction towards the initial spawner position
            Vector3 direction = (spawnerPosition.position - transform.position).normalized;

            // Move the projectile towards the initial spawner position
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            if (!inParry)
            {
                // Move the projectile towards the player using Lerp or any other logic you prefer
                transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime, Space.World);
            }
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

            case "Player":
                GetComponent<Renderer>().enabled = false;
                Destroy(gameObject);
                break;
        }
    }

    IEnumerator DestroyAfterSeconds(float projectileLifetime)
    {
        yield return new WaitForSeconds(projectileLifetime);
        Destroy(gameObject);
    }

    // Function to switch the projectile type to 'simple' after being parried
    public void SwitchToSimple()
    {
        wasParried = true;
    }
}
