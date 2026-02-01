using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{

    [SerializeField] WeaponSO weaponSO;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem muzzleFlash;
    StarterAssetsInputs starterAssetsInputs;

    Weapon currentWeapon;

    private float timeToNextShot = 0f;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        HandleShoot();
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon weapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        this.weaponSO = weaponSO;
        currentWeapon = weapon;
    }

    private void HandleShoot()
    {
        timeToNextShot += Time.deltaTime;
        if (timeToNextShot < weaponSO.FireRate)
        {
            return;

        }
        timeToNextShot = 0f;
        if (!starterAssetsInputs.shoot) return;

        currentWeapon.Shoot(weaponSO);
        animator.Play("Shoot", 0, 0f);
        RaycastHit hit;

        if (!weaponSO.IsAutomatic)
        {

            starterAssetsInputs.ShootInput(false);
        }
    }
}
