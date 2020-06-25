using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Combat combat;
    public HealthSystem health;

    void Update()
    {
        if(health.currentHealth<=30)
        {
            combat.isAttacking=true;
            StartCoroutine("Counterattack");
        }
    }

    IEnumerator Counterattack()
    {
        while(health.currentHealth<75)
        {
            combat.Attack();
            yield return new WaitForSeconds(2f);
        }
    }
}
