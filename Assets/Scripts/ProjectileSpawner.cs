using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public Transform spawnPos;

    public GameObject projectile;
    public GameObject explosionFX;

    public float timeBetweenShots = 4f;

    public AudioClip _deathClip;
    private AudioSource _deathSource;

    private Transform player;

    // Keep track of spawned projectiles
    private List<GameObject> spawnedProjectiles = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnProjectile());

        _deathSource = gameObject.AddComponent<AudioSource>();

        _deathSource.clip = _deathClip;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        spawnPos.LookAt(player);

        // Instantiate the projectile with the correct rotation
        GameObject thisProjectile = Instantiate(projectile, spawnPos.position, spawnPos.rotation);

        thisProjectile.GetComponent<BasicProjectile>().spawnerPosition = transform;

        // Add the spawned projectile to the list
        spawnedProjectiles.Add(thisProjectile);

        print("projectile set to chaser is currently " + thisProjectile.GetComponent<BasicProjectile>().wasParried);

        StartCoroutine(SpawnProjectile());
    }

    IEnumerator DeathRoutine()
    {
        _deathSource.Play();
        transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<ProjectileSpawner>().enabled = false;

        // Destroy all spawned projectiles
        foreach (GameObject projectile in spawnedProjectiles)
        {
            if (projectile != null)
            {
                Destroy(projectile);
            }
        }

        spawnedProjectiles.Clear(); // Clear the list

        explosionFX.transform.position = transform.position;
        explosionFX.GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(_deathClip.length);

        Destroy(gameObject);
    }

    public void DeathMethod()
    {
        StartCoroutine(DeathRoutine());
    }
}
