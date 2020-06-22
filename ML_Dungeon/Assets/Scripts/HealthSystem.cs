using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth=100f;
    public float currentHealth;


    public void TakeDamage(float damage)
    {
        currentHealth-=damage;
    }
}
