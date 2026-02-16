using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private HealthBar healthBar;

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        // Add death animation or effects here
        Destroy(gameObject);
    }
}
