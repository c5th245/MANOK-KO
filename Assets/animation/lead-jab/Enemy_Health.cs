using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int currentHealth;
    public int maxhealth;

    private void Start()
    {
      currentHealth = maxhealth;
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxhealth)
        {
            currentHealth = maxhealth;
        }
        else if (currentHealth <= 0)
        {
        Destroy(gameObject);    
        }
    }
}