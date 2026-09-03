using UnityEngine;

public class Health : MonoBehaviour
{
    public HealthSO healthSO;

    [SerializeField]private UnitDataSO unitDataSO;

    private void Start()
    {
        healthSO = ScriptableObject.CreateInstance<HealthSO>();
        SetMaxHealth();
        SetCurrentHealth(healthSO.maxHealth);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        healthSO.currentHealth -= damage;
        if (healthSO.currentHealth <= 0)
        {
            healthSO.currentHealth = 0;
            Die();
        }
        
    }

    private void SetMaxHealth()
    {
        healthSO.maxHealth = unitDataSO.unitHealth;
    }

    private void SetCurrentHealth(float healthToSet)
    {
        healthSO.currentHealth = healthToSet;

    }
}
