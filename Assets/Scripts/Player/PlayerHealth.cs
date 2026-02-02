using Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 5;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Transform shieldBarContainer;
    [SerializeField] Transform shielBarTemplate;

    int currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    private void Start()
    {
        UpdateUIHealth();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateUIHealth();
        if (currentHealth < 0)
        {
            weaponCamera.parent = null;
            deathVirtualCamera.Priority = 20;
            Destroy(gameObject);
        }
    }

    public void UpdateUIHealth()
    {
        foreach (Transform child in shieldBarContainer.transform)
        {
            if (child == shielBarTemplate) continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentHealth; i++)
        {
            Transform iconTransform = Instantiate(shielBarTemplate, shieldBarContainer.transform);
            iconTransform.gameObject.SetActive(true);
        }
    }
}
