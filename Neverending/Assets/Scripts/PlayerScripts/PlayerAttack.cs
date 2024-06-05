using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attack = 5.0f;

    public void DealDamage(EnemyHealth enemyHealth) {
        // need to consider:
        // 1. is target an enemy?
        // 2. is target within range?
        // return WithinAttackRange && gameObject.CompareTag("Enemy");
        enemyHealth.TakeDamage(attack);
    }

    private bool CanDealDamage() {
        
        return true;
    }

    private bool WithinAttackRange() {
        return true;
    }

    /*
    trigger attack --> find enemy gameobjects within attack range --> 
    deal damage to each of them
    */
}
