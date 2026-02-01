using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{

    [SerializeField] WeaponSO currentWeaponSO;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] private GameObject zoomInImage;

    public TextMeshProUGUI ammoText;
    public WeaponSO WeaponSO => currentWeaponSO;

    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    float defaultFOV;
    int currentAmmo;



    [SerializeField] private float timeToNextShot = 0f;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
    }

    private void Update()
    {
        if (!currentWeapon) return;
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }
        ammoText.text = currentAmmo.ToString();
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon weapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        this.currentWeaponSO = weaponSO;
        currentWeapon = weapon;
        AdjustAmmo(currentWeaponSO.MagazineSize);

    }

    private void HandleShoot()
    {
        timeToNextShot += Time.deltaTime;
        if (timeToNextShot < currentWeaponSO.FireRate || currentAmmo <= 0)
        {
            return;

        }
        if (!starterAssetsInputs.shoot) return;
        timeToNextShot = 0f;
        AdjustAmmo(-1);
        currentWeapon.Shoot(currentWeaponSO);
        animator.Play("Shoot", 0, 0f);
        RaycastHit hit;

        if (!currentWeaponSO.IsAutomatic)
        {

            starterAssetsInputs.ShootInput(false);
        }
    }

    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom)
        {
            return;
        }

        if (starterAssetsInputs.zoom)
        {
            zoomInImage.SetActive(true);
            playerFollowCamera.m_Lens.FieldOfView = Mathf.Lerp(playerFollowCamera.m_Lens.FieldOfView, defaultFOV - currentWeaponSO.ZoomAmount, Time.deltaTime * 10f);
        }
        else
        {
            zoomInImage.SetActive(false);
            playerFollowCamera.m_Lens.FieldOfView = Mathf.Lerp(playerFollowCamera.m_Lens.FieldOfView, defaultFOV, Time.deltaTime * 10f);
        }
    }
}
