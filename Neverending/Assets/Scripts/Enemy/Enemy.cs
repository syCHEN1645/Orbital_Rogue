using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // list of all Enemies alive
    public static List<Enemy> enemyList;
    public Animator animator;
    protected EnemyHealth enemyHealth;
    protected GameObject player;
    protected PlayerHealth playerHealth;
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
<<<<<<< HEAD:Neverending/Assets/Scripts/EnemyScripts/Enemy.cs
    protected float spriteScale;
=======
    protected bool canMove = true;
>>>>>>> New-Top-Down:Neverending/Assets/Scripts/Enemy/Enemy.cs

    protected virtual void InitialiseEnemy() {
        if (enemyList == null) {
            enemyList = new List<Enemy>();
        }
        player = GameObject.Find("Player");
        enemyHealth = gameObject.GetComponent<EnemyHealth>();
        originalPosition = transform.position;
        isAttacking = false;
        // set initial dir being left
        dir = 'l';
        // add to enemyList
        AddThis();
    }

    protected void AddThis() {
        enemyList.Add(this);
    }
    protected void RemoveThis() {
        enemyList.Remove(this);
    }

    public virtual void Attack() {
    }

    public virtual void Injure() {
    }

    public virtual void Die() {
        RemoveThis();
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }

    protected virtual bool WithinAttackRange() {
        if (player == null) {
            return false;
        }
        return Vector2.Distance(player.transform.position, gameObject.transform.position) <= attackRange;
    }

    protected virtual IEnumerator AttackPlayer() {
        isAttacking = true;
        Attack();
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }
    protected virtual void InflictDamage() {
        playerHealth = player.GetComponent<PlayerHealth>();
        if (WithinAttackRange() && !playerHealth.IsDead()) {
            playerHealth.TakeDamage(attack);
        }
    }

    protected virtual void BodyDisappear() {
        Destroy(gameObject);
    }
}
