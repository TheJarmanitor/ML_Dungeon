using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth=100f;
    public float currentHealth;
    public float healStart=5f;
    public float healingRate=2f;

    bool IsRegen=false;
    bool damaged=false;

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
        damaged=true;
    }

    public void Heal()
    {
        if(damaged && !IsRegen)
        {
            StartCoroutine("Regen");
            damaged=false;
        }
        if(damaged && IsRegen)
        {
            StopCoroutine("Regen");
            IsRegen=false;
        }

    }

    IEnumerator Regen()
    {
        IsRegen=true;

        yield return new WaitForSeconds(healStart);

        while(currentHealth<maxHealth)
        {
            currentHealth+=healingRate;
            healthBar.SetHealth(currentHealth);
            yield return new WaitForSeconds(1f);
        }
        IsRegen=false;
    }
}
