using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;
    
    [SerializeField]
    private Image fillImage;
    
    [SerializeField]
    private Color fullHealthColor = Color.green;
    
    [SerializeField]
    private Color lowHealthColor = Color.red;
    
    public void SetMaxHealth(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        UpdateHealthColor();
    }
    
    public void SetHealth(float health)
    {
        healthSlider.value = health;
        UpdateHealthColor();
    }
    
    void UpdateHealthColor()
    {
        if (fillImage != null)
        {
            float healthPercent = healthSlider.value / healthSlider.maxValue;
            fillImage.color = Color.Lerp(lowHealthColor, fullHealthColor, healthPercent);
        }
    }
}
