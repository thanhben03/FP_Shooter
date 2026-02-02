using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{

    [SerializeField] GameObject robotPrefab;
    [SerializeField] float spawnTime = 3f;
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
            Debug.Log("Spawn Gate");
            Instantiate(robotPrefab, spawnPoint);
            yield return new WaitForSeconds(spawnTime);

        }

        yield return null;
    }
}
