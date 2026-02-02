using UnityEngine;

public class AmmoPickup : Pickup
{
    private Transform spawnPoint;
    private AmmoSpawner spawner;

    public void SetSpawnPoint(Transform point, AmmoSpawner ammoSpawner)
    {
        spawnPoint = point;
        spawner = ammoSpawner;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
        if (activeWeapon != null)
        {
            OnPickup(activeWeapon);
        }

        gameObject.SetActive(false);

        if (spawner != null && spawnPoint != null)
        {
            spawner.Respawn(spawnPoint);
        }
    }

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        WeaponSO weaponSO = activeWeapon.WeaponSO;
        if (weaponSO == null) return;
        activeWeapon.AdjustAmmo(weaponSO.MagazineSize);
    }


}
