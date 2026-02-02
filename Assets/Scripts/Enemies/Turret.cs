using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] float fireRate = 2f;
    [SerializeField] int damage = 2;

    PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(FireRoutine());
    }

    void Update()
    {
        if (playerHealth != null)
        {

            turretHead.LookAt(playerHealth.transform);
        }
    }

    IEnumerator FireRoutine()
    {
        while (playerHealth)
        {
            yield return new WaitForSeconds(fireRate);
            GameObject projectileObj = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            projectile.Init(damage);
            if (playerHealth)
            {
                projectile.transform.LookAt(playerHealth.transform.position);
                
            }
        }

        yield return null;
    }

}
