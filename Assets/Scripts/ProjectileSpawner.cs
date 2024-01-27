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

    private void Start()
    {
        StartCoroutine(SpawnProjectile());

        _deathSource = gameObject.AddComponent<AudioSource>();

        _deathSource.clip = _deathClip;
    }

    IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        // Instantiate the projectile with the correct rotation
        GameObject thisProjectile = Instantiate(projectile, spawnPos.position, spawnPos.rotation);

        thisProjectile.GetComponent<BasicProjectile>().spawnerPosition = transform;

        StartCoroutine(SpawnProjectile());
    }

    IEnumerator DeathRoutine()
    {
        _deathSource.Play();
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<ProjectileSpawner>().enabled = false;

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
