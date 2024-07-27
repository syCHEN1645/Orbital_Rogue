using System.Collections;
using UnityEngine;

public class EvilWizard : Enemy
{
    // enemy stops at a distance of stopMovingDistance away from Player
    // enemy starts hunting if Player is closer than huntRange
    [SerializeField] protected float healthBarOffset, attackOffsetX, attackOffsetY, huntRange;
    [SerializeField] protected float attackDamage, damageInterval, maxAttackDuration;
    
    // an empty object indicating centre of boss
    public GameObject centre;
    public GameObject attackMarker;
    protected float centreOffset;
    // protected Vector3 previousVector;
    private bool canAttack = true;
    private bool dealingDamage = false;
    private bool damageCooldown = false;
    private int facingDirection;
    private float workspace;

    protected override void InitialiseEnemy()
    {
        base.InitialiseEnemy();
        speed = 20f;
        // attack is by default referring to melee attack if both melee and ranged attacks exist
        attackDamage = 3f;
        attackRange = 2.5f;
        attackInterval = 3f;
        maxAttackDuration = 5f;
        damageInterval = 0.5f;

        spriteScale = 4.0f;

        healthBarOffset = 1.4f;
        attackOffsetX = 0.5f;
        attackOffsetY = 0.1f;
        huntRange = 15.0f;

        enemyHealth.SetDefense(80);
        enemyHealth.SetHealth(100);
        enemyHealth.SetMaxHealth(100);
        enemyHealth.HealthBarUpdate();

        //previousVector = GetUnitVectorTowardsPlayer();
        
    }

    public override void Attack()
    {
        animator.SetBool("Attack", true);
    }

    public void CancelAttack()
    {
        animator.SetBool("Attack", false);
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

    void Start()
    {
        InitialiseEnemy();
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
                if (playerData.isSLowed) {
                    playerData.RecoverSpeed();
                }
                CancelAttack();
                Die();
            } else {
                if (player != null) {
                    if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= huntRange) {
                        HuntPlayer();
                    } else {
                        // idle around
                    }
                }
            }           
        }
    }

    void FixedUpdate()
    {
        if (dealingDamage && !damageCooldown && playerData != null) {
            Debug.Log("deal damage");
            playerData.TakeDamage(attackDamage);
            StartCoroutine(DamageInterval());
            StartCoroutine(playerData.TemporarySlow(2.0f, 0.5f));
        }
    }

    private IEnumerator DamageInterval()
    {
        damageCooldown = true;
        yield return new WaitForSeconds(damageInterval);
        damageCooldown = false;
    }

    protected void HuntPlayer()
    {  
        if (!canAttack || isAttacking) {
            StopWalk();
        } else {
            if (Mathf.Abs(player.transform.position.x - attackMarker.transform.position.x) < attackOffsetX
                    && Mathf.Abs(player.transform.position.y - attackMarker.transform.position.y) < attackOffsetY) {
                //Debug.Log("Boss Attack");
                isAttacking = true;
                LockMovement();
                Attack();
            } else {
                //(Vector2.Distance(player.transform.position, centre.transform.position) > stopMovingDistance) 
                MoveTowardsPlayer();  
            }
        }   
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerData = other.gameObject.GetComponent<PlayerData>();
            dealingDamage = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerData = other.gameObject.GetComponent<PlayerData>();
            dealingDamage = false;
        }
    }

    public IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackInterval);
        canAttack = true;
        Debug.Log("isAttacking" + isAttacking + ", canAttack" + canAttack);
    }

    public void LockMovement()
    {
        StopWalk();
        if (speed != 0) {
            workspace = speed;
        }
        this.speed = 0;
        Debug.Log(workspace);
    }

    public void UnlockMovement()
    {
        speed = workspace;
    }

    public void AttackEnd()
    {
        if (!dealingDamage) {
            CancelAttack();
            UnlockMovement();
            isAttacking = false;
            StartCoroutine(AttackCooldown()); 
            AttackFinishedTrigger();
        }
    }

       protected bool WithinHuntRange() {
        if (Vector2.Distance(player.transform.position, centre.transform.position) <= huntRange) {
            return true;
        }
        return false;
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

        facingDirection = (int)Mathf.Sign(animator.transform.localScale.x);
        var directionVector = player.transform.position - centre.transform.position;
        // Debug.Log(directionVector.x);
        // Debug.Log("facingDirection: " + facingDirection);
        // flip
        if (directionVector.x * facingDirection < -1) {
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
        // previousVector = directionVector;
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

    protected Vector3 GetUnitVectorTowardsPlayer() 
    {
        // x = x, y = y, z = 0
        return Vector3.Normalize(player.transform.position - attackMarker.transform.position);
    }
}

