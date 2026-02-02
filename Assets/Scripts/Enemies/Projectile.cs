using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] GameObject projectileVFXPrefab;

    Rigidbody rb;
    int damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    public void Init(int amount)
    {
        damage = amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        playerHealth?.TakeDamage(damage);
        Instantiate(projectileVFXPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
