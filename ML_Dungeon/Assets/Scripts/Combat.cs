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

    public bool isAttacking;


   public void Attack()
    {
        if(isAttacking)
        {
            anim.SetTrigger("Attack");

            Collider2D[] hitEnemies=Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<HealthSystem>().TakeDamage(attackDamage);
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
