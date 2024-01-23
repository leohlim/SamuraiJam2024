using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject projectile;
    public float timeBetweenShots = 4f;

    public GameObject explosionFX;

    [HideInInspector]
    public AudioSource _blastSource;

    public AudioClip _blastClip;

    private void Start()
    {
        StartCoroutine(SpawnProjectile());

        _blastSource = gameObject.AddComponent<AudioSource>();

        _blastSource.clip = _blastClip;
    }

    IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        // Instantiate the projectile with the correct rotation
        GameObject thisProjectile = Instantiate(projectile, spawnPos.position, spawnPos.rotation);

        thisProjectile.GetComponent<BasicProjectile>().spawnerPosition = transform;

        StartCoroutine(SpawnProjectile());
    }
}
