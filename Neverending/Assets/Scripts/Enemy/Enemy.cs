using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // list of all Enemies alive
    public static List<Enemy> enemyList;
    public Animator animator;
    protected EnemyHealth enemyHealth;
    protected GameObject player;
    protected PlayerData playerData;
    // dir: enemy is facing this direction: l->left, r->right.
    protected char dir;
    private Collider2D weaponCollider;
    // originalPosition: where this enemy is spawned
    protected Vector3 originalPosition;
    // how many items to drop
    [SerializeField] protected int itemsCount;
    // this list contains all rewards to be dropped after enemy is defeated
    [SerializeField] protected List<GameObject> itemsToDrop;
    // attackInterval: time interval between attacks
    [SerializeField] protected float speed, attack, attackRange, attackInterval;  
    [SerializeField] protected float spriteScale;   
    protected bool isAttacking;
    protected bool stop = false;

    protected virtual void InitialiseEnemy() {
        if (enemyList == null) {
            enemyList = new List<Enemy>();
        }
        player = GameObject.FindGameObjectWithTag("Player");
        playerData = player.GetComponent<PlayerData>();

        enemyHealth = gameObject.GetComponent<EnemyHealth>();
        weaponCollider = GameObject.Find("AttackRange")?.GetComponent<Collider2D>();
        // Debug.Log(weaponCollider == null);
        originalPosition = transform.position;
        isAttacking = false;
        // set initial dir being left
        dir = 'l';
        // add to enemyList
        AddThis();

        for (int i = 0; i < itemsCount; i++) {
            int index = Random.Range(0, PGPararmeters.itemList.Length);
            itemsToDrop.Add(PGPararmeters.itemList[index]);
        }
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
        DropItems();
        RemoveThis();
        GameManager.killCount++;
        stop = true;
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
        //playerData.TakeDamage(attack);
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
    }

    public void AttackTrigger() {
        weaponCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerData = other.gameObject.GetComponent<PlayerData>();
            //Debug.Log("Deal Damage");
            playerData?.TakeDamage(attack);
        }
    }

    public void AttackFinishedTrigger()
    {
        weaponCollider.enabled = false;
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

    public void AddKey() {
        itemsToDrop.Add(PGPararmeters.portalKey);
    }
}
