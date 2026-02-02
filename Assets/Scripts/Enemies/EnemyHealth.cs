using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject explosionVFX;
    [SerializeField] int startingHealth = 3;

    int currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;

    }

    private void Start()
    {
        GameManager.Instance.AdjustEnemyLeftUI(gameObject, 1);

    }


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        GameManager.Instance.AdjustEnemyLeftUI(gameObject, -1);
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
