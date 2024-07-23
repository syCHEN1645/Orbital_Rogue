using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBlueSlime1 : Enemy
{
    protected override void InitialiseEnemy()
    {
        base.InitialiseEnemy();
        speed = 0.0f;
        attack = 20.0f;
        attackRange = 0.0f;
        attackInterval = 2.0f;
    }
    void Start()
    {
        InitialiseEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop) {
            if (enemyHealth.IsDead()) {
                Die();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        // if collide with player
        if (other.gameObject.CompareTag("Player")) {
            // deal damage to player
            // remmber StartCorourine!!!
            StartCoroutine(AttackPlayer());
            // other.gameObject.TakeDamage();
        }
    }
    public override void Die() {
        base.Die();
    }
}

