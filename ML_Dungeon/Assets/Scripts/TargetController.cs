using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Combat combat;
    public HealthSystem health;
    public bool counterAttack=false;

    void Update()
    {
        if(health.currentHealth==health.maxHealth)
        {
            counterAttack=false;
        }
        if(health.currentHealth<=50)
        {
            if(counterAttack==false)
            {
                combat.isAttacking=true;
                combat.Attack();
                counterAttack=true;
            }
        }
    }
}
