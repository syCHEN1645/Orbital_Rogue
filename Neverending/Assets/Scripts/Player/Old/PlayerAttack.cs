using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attack = 5.0f;
    private float attackRange = 1.6f;
    private float attackInterval = 0.8f;
    private bool isAttacking = false;
    public IEnumerator DealDamage(EnemyHealth enemyHealth) {
        // need to consider:
        // 1. is target an enemy?
        // 2. is target within range?
        // return WithinAttackRange && gameObject.CompareTag("Enemy");
        isAttacking = true;
        enemyHealth.TakeDamage(attack);
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }

    public bool IsAttacking() {
        return isAttacking;
    }
    public bool WithinAttackRange(Enemy enemy) {
        if (enemy != null) {
            return (Vector2.Distance(enemy.transform.position, transform.position) <= attackRange);
        }
        return false;
    }

    /*
    trigger attack --> find enemy gameobjects within attack range --> 
    deal damage to each of them
    */
}
