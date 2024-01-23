using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject projectile;
    public float timeBetweenShots = 4f;

    private void Start()
    {
        StartCoroutine(SpawnProjectile());
    }

    IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        // Instantiate the projectile with the correct rotation
        GameObject thisProjectile = Instantiate(projectile, spawnPos.position, spawnPos.rotation);

        thisProjectile.GetComponent<BasicProjectile>().spawnerPosition = spawnPos;

        StartCoroutine(SpawnProjectile());
    }
}
