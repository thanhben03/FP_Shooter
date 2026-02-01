using UnityEngine;

public class AmmoPickup : Pickup
{
    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        WeaponSO weaponSO = activeWeapon.WeaponSO;
        if (weaponSO == null) return;
        activeWeapon.AdjustAmmo(weaponSO.MagazineSize);
    }


}
