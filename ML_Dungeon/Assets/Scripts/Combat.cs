using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    public Animator anim;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackDamage=20f;
    public float attackRange=0.5f;
    public float attackRate=2f;
    float nextAttackTime=0f;

    public bool isAttacking;


    public void Attack()
    {
        if(Time.time>=nextAttackTime)
        {
            if(isAttacking)
            {
                anim.SetTrigger("Attack");
                Collider2D[] hitEnemies=Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                foreach(Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<HealthSystem>().TakeDamage(attackDamage);
                }
                nextAttackTime=Time.time+1f/attackRate;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint==null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
