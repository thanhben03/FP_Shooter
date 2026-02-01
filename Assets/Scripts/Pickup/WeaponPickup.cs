using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] private WeaponSO weaponSO;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.SwitchWeapon(weaponSO);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
    //        if (activeWeapon != null)
    //        {
    //            activeWeapon.SwitchWeapon(weaponSO);
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}
