using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth=100f;
    public float currentHealth;

    public HealthBar healthBar;

    public void ResetHealth()
    {
        currentHealth=maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth-=damage;

        healthBar.SetHealth(currentHealth);
    }
}
