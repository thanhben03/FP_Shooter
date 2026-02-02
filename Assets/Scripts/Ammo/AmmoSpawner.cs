using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public static AmmoSpawner Instance;

    public Transform[] spawnPoints;
    public float respawnTime = 10f;
    [SerializeField] private List<Transform> availablePoints = new List<Transform>();
    public int spawnCount = 5;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        availablePoints.AddRange(spawnPoints);
        SpawnRandomAmmo();

    }


    public void SpawnRandomAmmo()
    {
        if (availablePoints.Count == 0) return;

        int count = Mathf.Min(spawnCount, availablePoints.Count);

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, availablePoints.Count);
            Transform point = availablePoints[index];
            availablePoints.RemoveAt(index);

            GameObject ammo = AmmoPool.Instance.GetAmmo();
            ammo.transform.position = point.position;
            ammo.transform.rotation = point.rotation;

            ammo.GetComponent<AmmoPickup>().SetSpawnPoint(point, this);
            ammo.SetActive(true);
            Debug.Log("Spawned ammo at: " + point.position);
        }
    }

    public void Respawn(Transform point)
    {
        StartCoroutine(RespawnCoroutine(point));
    }

    IEnumerator RespawnCoroutine(Transform point)
    {
        yield return new WaitForSeconds(respawnTime);
        availablePoints.Add(point);
        SpawnRandomAmmo();
    }
}
