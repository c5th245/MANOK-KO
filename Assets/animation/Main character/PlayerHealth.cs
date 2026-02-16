using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
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

        Debug.Log("Player health: " + currentHealth + "/" + maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        Debug.Log("Player took " + damage + " damage. Health: " + currentHealth + "/" + maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        Debug.Log("Player healed " + amount + ". Health: " + currentHealth + "/" + maxHealth);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    void Die()
    {
        Debug.Log("Player died!");
        gameObject.SetActive(false);
    }
}
