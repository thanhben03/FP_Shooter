using Cinemachine;
using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask interactionLayers;

    CinemachineImpulseSource impluseSource;

    private void Awake()
    {
        impluseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {

        RaycastHit hit;

        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore);
        muzzleFlash.Play();
        impluseSource.GenerateImpulse();
        if (hit.collider != null)
        {

            Instantiate(weaponSO.hitVFX, hit.point, Quaternion.identity);
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
