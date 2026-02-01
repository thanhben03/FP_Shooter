using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{

    [SerializeField] WeaponSO weaponSO;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] private GameObject zoomInImage;
    StarterAssetsInputs starterAssetsInputs;

    Weapon currentWeapon;
    float defaultFOV;

    [SerializeField] private float timeToNextShot = 0f;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        HandleShoot();
        HandleZoom();
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
        if (!starterAssetsInputs.shoot) return;
        timeToNextShot = 0f;

        currentWeapon.Shoot(weaponSO);
        animator.Play("Shoot", 0, 0f);
        RaycastHit hit;

        if (!weaponSO.IsAutomatic)
        {

            starterAssetsInputs.ShootInput(false);
        }
    }

    void HandleZoom()
    {
        if (!weaponSO.CanZoom)
        {
            return;
        }

        if (starterAssetsInputs.zoom)
        {
            zoomInImage.SetActive(true);
            playerFollowCamera.m_Lens.FieldOfView = Mathf.Lerp(playerFollowCamera.m_Lens.FieldOfView, defaultFOV - weaponSO.ZoomAmount, Time.deltaTime * 10f);
        }
        else
        {
            zoomInImage.SetActive(false);
            playerFollowCamera.m_Lens.FieldOfView = Mathf.Lerp(playerFollowCamera.m_Lens.FieldOfView, defaultFOV, Time.deltaTime * 10f);
        }
    }
}
