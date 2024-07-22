using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // list of all Enemies alive
    public static List<Enemy> enemyList;
    public Animator animator;
    protected EnemyHealth enemyHealth;
    protected GameObject player;
    protected PlayerData playerData;
    // originalPosition: where this enemy is spawned
    protected Vector2 originalPosition;
    [SerializeField]
    // attackInterval: time interval between attacks
    protected float speed, attack, attackRange, attackInterval;

    // dir: enemy is facing this direction: l->left, r->right.
    protected char dir;
    protected bool isAttacking;
    [SerializeField] protected float spriteScale;

    [SerializeField]
    // this list contains all rewards to be dropped after enemy is defeated
    protected List<GameObject> itemsToDrop;

    protected virtual void InitialiseEnemy() {
        if (enemyList == null) {
            enemyList = new List<Enemy>();
        }
        player = GameObject.FindGameObjectWithTag("Player");
        playerData = player.GetComponent<PlayerData>();

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
        StartCoroutine(BodyDisappear());
    }
    protected virtual bool WithinAttackRange(float range) {
        if (player == null) {
            return false;
        }
        return Vector2.Distance(player.transform.position, 
                gameObject.transform.position) <= range;
    }
    protected virtual IEnumerator AttackPlayer() {
        isAttacking = true;
        Attack();
        playerData.TakeDamage(attack);
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }
    protected virtual IEnumerator BodyDisappear() {
        // wait for 2 seconds then body will disappear.
        yield return new WaitForSeconds(2);
        // remove sprite
        Destroy(gameObject);
    }

    protected void DropItems() {
        for (int i = 0; i < itemsToDrop.Count; i++) {
            // instantiate items at enemy's pos
            Instantiate(itemsToDrop[i], gameObject.transform.position, Quaternion.identity);
        }
    }
}
