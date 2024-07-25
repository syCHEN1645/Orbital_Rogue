using System.Collections;
using UnityEngine;

public class EvilWizard2 : Enemy
{
    // enemy stops at a distance of stopMovingDistance away from Player
    // enemy starts hunting if Player is closer than huntRange
    [SerializeField] protected float healthBarOffset, stopMovingDistance, huntRange;
    
    // an empty object indicating centre of boss
    public GameObject centre;
    //private float spellPositionOffset;
    protected float centreOffset;
    protected Vector3 previousVector;
    private float workspace;

    void Start()
    {
        InitialiseEnemy();
        
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
        speed = 7f;
        // attack is by default referring to melee attack if both melee and ranged attacks exist
        attack = 15.0f;
        attackRange = 1.5f;

        attackInterval = 5f;
        
        spriteScale = 4.0f;

        healthBarOffset = 1.4f;
        stopMovingDistance = 0.5f;
        huntRange = 15.0f;

        enemyHealth.SetDefense(30);
        enemyHealth.SetHealth(100);
        enemyHealth.SetMaxHealth(100);
        enemyHealth.HealthBarUpdate();

        //canAttack = true;
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
            if (Vector2.Distance(player.transform.position, centre.transform.position) > stopMovingDistance) {
                MoveTowardsPlayer();
            } if (WithinAttackRange(attackRange)) {
                Debug.Log("Attack");
                StopWalk();
                StartCoroutine(AttackPlayer());
            } else {
                
            }
        }
    }

    public void LockMovement()
    {
        StopWalk();
        if (speed != 0) {
            workspace = speed;
        }
        this.speed = 0;
    }

    public void UnlockMovement()
    {
        speed = workspace;
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
        if (unitVector.x * previousVector.x < 0) {
            // dir has changed
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
}

