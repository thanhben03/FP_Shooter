using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour
{
    public static AmmoPool Instance;

    [Header("Pool Settings")]
    public GameObject ammoPrefab;
    public int poolSize = 5;

    private List<GameObject> ammoPool = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammo = Instantiate(ammoPrefab, transform);
            ammo.SetActive(false);
            ammoPool.Add(ammo);
        }
    }

    public GameObject GetAmmo()
    {
        foreach (var ammo in ammoPool)
        {
            if (!ammo.activeInHierarchy)
                return ammo;
        }

        GameObject newAmmo = Instantiate(ammoPrefab, transform);
        newAmmo.SetActive(false);
        ammoPool.Add(newAmmo);
        return newAmmo;
    }
}
