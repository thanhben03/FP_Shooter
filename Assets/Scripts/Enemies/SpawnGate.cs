using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{

    [SerializeField] GameObject robotPrefab;
    [SerializeField] float spawnTime = 5f;
    [SerializeField] Transform spawnPoint;

    PlayerHealth playerHealth;
    private void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (playerHealth)
        {
            Instantiate(robotPrefab, spawnPoint);

            yield return new WaitForSeconds(spawnTime);
        }

        yield return null;
    }
}
