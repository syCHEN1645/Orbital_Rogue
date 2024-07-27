using System.Collections;
using UnityEngine;

public class BringerOfDeath : Enemy
{
    // enemy stops at a distance of stopMovingDistance away from Player
    // enemy starts hunting if Player is closer than huntRange
    [SerializeField] protected float healthBarOffset, stopMovingDistance, huntRange, rangedAttackRange;
    [SerializeField] protected float rangedAttackDamage;
    [SerializeField] protected float rangedAttackInterval;
    [SerializeField] private GameObject spellPrefab;
    
    // an empty object indicating centre of boss
    public GameObject centre;
    private float spellPositionOffset;
    protected float centreOffset;
    protected Vector3 previousVector;
    private bool canRangedAttack;
    private float workspace;

    void Start()
    {
        InitialiseEnemy();
        // correct dir
        // Flip();
        // Attack();
        // Cast();
        // Spell();
        // Walk();
        // Injure();
    }

    void Update()
    {
        if (!stop) {
            if (enemyHealth.IsDead()) {
                Die();
            } else {
                if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= huntRange) {
                    HuntPlayer();
                } else {
                    // idle around
                }
            }
        }
    }

    protected override void InitialiseEnemy()
    {
        base.InitialiseEnemy();
        speed = 2.0f;
        // attack is by default referring to melee attack if both melee and ranged attacks exist
        attack = 30.0f;
        attackRange = 1.5f;
        attackInterval = 1.5f;
        
        spriteScale = 4.0f;

        healthBarOffset = 1.4f;
        stopMovingDistance = 0.5f;
        huntRange = 15.0f;
        rangedAttackInterval = 12.0f;
        rangedAttackDamage = 50.0f;
        rangedAttackRange = 5.0f;

        enemyHealth.SetDefense(30);
        enemyHealth.SetHealth(100);
        enemyHealth.SetMaxHealth(100);
        enemyHealth.HealthBarUpdate();

        canRangedAttack = true;
        spellPositionOffset = 1.24f;
        previousVector = GetUnitVectorTowardsPlayer();
        
    }

    public override void Attack()
    {
        animator.SetTrigger("Attack");
    }
    
    protected void Cast() {
        animator.SetTrigger("Cast");
    }
    protected void Spell() {
        animator.SetTrigger("Spell");
    }

    protected void Walk() {
        animator.SetBool("Walk", true);
    }

    protected void StopWalk() {
        animator.SetBool("Walk", false);
    }

    public override void Injure() {
        StartCoroutine(LockMovement());
        isAttacking = false;
        animator.SetTrigger("Hurt");
    }

    public override void Die() {
        base.Die();
        // animate death
        animator.SetTrigger("Death");
    }

    protected bool WithinHuntRange() {
        if (Vector2.Distance(player.transform.position, centre.transform.position) <= huntRange) {
            return true;
        }
        return false;
    }

    protected void HuntPlayer()
    {   
        if (isAttacking) {
            // if attacking, do not move
            StopWalk();
        } else {
            // if not attacking, move towards Player
            if (Vector2.Distance(player.transform.position, centre.transform.position) < stopMovingDistance) {
                Debug.Log("Attack");
                StopWalk();
                StartCoroutine(AttackPlayer());
            } else if (canRangedAttack && (WithinAttackRange(rangedAttackRange))) {
                StopWalk();
                StartCoroutine(RangedAttackPlayer());  
            } else {
                MoveTowardsPlayer();
            }
        }
    }

    protected void MoveTowardsPlayer()
    {
        // vector pointing to Player
        // Debug.Log("pos " + centre.transform.position.x);
        // Debug.Log("local " + centre.transform.localPosition.x);
        var unitVector = GetUnitVectorTowardsPlayer();
        gameObject.transform.Translate(
            speed * Time.deltaTime * unitVector.x, 
            speed * Time.deltaTime * unitVector.y, 
            0);
        
        // flip
        if (unitVector.x * previousVector.x < -0.2) {
            // dir has changed
            Debug.Log("Flip");
            Flip();
            // left / right
            if (unitVector.x > 0) {
                dir = 'r';
            } else {
                dir = 'l';
            }
        }

        // animation
        Walk();
        // current vector becomes previous vector
        previousVector = unitVector;
    }

    private void Flip()
    {
        animator.transform.localScale = new Vector3(
            -animator.transform.localScale.x, 
            animator.transform.localScale.y, 
            animator.transform.localScale.z);
        // this line if put in Start() will return 0, maybe due to
        centreOffset = Mathf.Abs(gameObject.transform.position.x - centre.transform.position.x);
        if (animator.transform.localScale.x < 0) {
            // if now x < 0 i.e. face left, shift left
            gameObject.transform.Translate(-2 * centreOffset, 0 , 0);
            //Debug.Log(-2 * centreOffset);
        } else {
            // if now x > 0 i.e. face right, shift right
            gameObject.transform.Translate(2 * centreOffset, 0 , 0);
            //Debug.Log(2 * centreOffset);
        }
    }

    protected Vector3 GetUnitVectorTowardsPlayer() {
        // x = x, y = y, z = 0
        return Vector3.Normalize(player.transform.position - centre.transform.position);
    }

    protected IEnumerator RangedAttackPlayer() {
        isAttacking = true;
        canRangedAttack = false;
        // ranged attack animation
        Cast();

        yield return new WaitForSeconds(rangedAttackInterval);
        canRangedAttack = true;
    }

    public void OnRangedAttack() {
        Debug.Log("ranged attack");
        isAttacking = false;
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        Vector3 position = new Vector3(0, spellPositionOffset, 0) + player.transform.position;
        StartCoroutine(playerData.TemporarySlow(2.0f, 2.0f));
        GameObject spell = Instantiate(spellPrefab, position, rot);
        spell.GetComponent<Spell>().SetDamage(rangedAttackDamage);
    }

    protected IEnumerator LockMovement()
    {
        float stopDuration = 0.3f;
        StopWalk();
        if (speed != 0) {
            workspace = speed;
        }
        this.speed = 0;
        yield return new WaitForSeconds(stopDuration);
        speed = workspace;
    }

    /*public void UnlockMovement()
    {
        speed = workspace;
        Debug.Log(speed);
    }*/
}

