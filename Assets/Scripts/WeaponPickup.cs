using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            if (activeWeapon != null)
            {
                activeWeapon.SwitchWeapon(weaponSO);
                Destroy(gameObject);
            }
        }
    }
}
