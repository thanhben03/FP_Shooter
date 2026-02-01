using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem muzzleFlash;
    StarterAssetsInputs starterAssetsInputs;
    [SerializeField] int damageAmount = 1;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

    private void Update()
    {
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;
        muzzleFlash.Play();
        animator.Play("Shoot", 0, 0f);
        RaycastHit hit;
        starterAssetsInputs.ShootInput(false);

        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);
        if (hit.collider != null)
        {
            Instantiate(hitVFX, hit.point, Quaternion.identity);
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            enemyHealth?.TakeDamage(damageAmount);
        }
    }
}
