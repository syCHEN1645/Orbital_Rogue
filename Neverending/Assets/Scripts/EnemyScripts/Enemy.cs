using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    protected EnemyHealth enemyHealth;
    protected GameObject player;
    // originalPosition: where this enemy is spawned
    protected Vector2 originalPosition;
    protected float speed;
    protected float attack;
    protected float attackRange;
    // attackInterval: time interval between attacks
    protected float attackInterval;
    // dir: enemy is facing this direction: l-left, r-right.
    protected char dir;
    protected bool isAttacking;

    protected virtual void SetParameters() {
        player = GameObject.Find("Player");
        enemyHealth = gameObject.GetComponent<EnemyHealth>();
        originalPosition = transform.position;
        isAttacking = false;
        // set initial dir being left
        dir = 'l';
    }
    public virtual void Attack() {
    }

    public virtual void Injure() {
    }

    public virtual void Die() {
    }
    protected virtual bool WithinAttackRange() {
        return Vector3.Distance(player.transform.position, gameObject.transform.position) <= attackRange;
    }
}
