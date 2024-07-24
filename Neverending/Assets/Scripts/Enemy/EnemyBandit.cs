using UnityEngine;

public class EnemyBandit : Enemy
{
    protected Vector3 patrolDir;
    // true: can change dir
    // false: continue in cur dir
    protected bool stepFinished;
    // distance to travel before change dir, i.e. step length
    protected float stepLen;

    [SerializeField]
    protected float patrolRange, healthBarOffset, huntRange;
    private float workspace;
    
    protected override void InitialiseEnemy() {
        base.InitialiseEnemy();
        speed = 0.7f;
        attack = 15.0f;
        attackRange = 1.0f;
        attackInterval = 1.0f;
        spriteScale = 1.0f;
        patrolRange = 5.0f;
        healthBarOffset = 1.4f;
        huntRange = 2.0f;

        originalPosition = gameObject.transform.position;
        patrolDir = Vector3.zero;
        stepLen = 0;
    }
    void Start()
    {
        InitialiseEnemy();      
        animator.transform.localScale = new Vector3(-spriteScale, spriteScale, spriteScale);
    }

    // Update is called once per frame
    void Update()
    {
        // enemyHealth.healthBar.transform.position = transform.position + new Vector3(0, healthBarOffset, 0);
        if (!stop) {
            if (enemyHealth.IsDead()) {
                Die();
            } else {
                if (!WithinAttackRange(attackRange)) {
                    // not in range, patrol
                    RandomPatrol();
                } else if (!isAttacking) {
                    // is not attacking, then attack
                    StartCoroutine(AttackPlayer());
                }
            }
        }
    }

    public override void Attack() {
        base.Attack();
        animator.SetTrigger("Attack");
    }

    public override void Injure() {
        base.Injure();
        animator.SetTrigger("Hurt");
    }

    public override void Die() {
        base.Die();
        // animate death
        animator.SetTrigger("Death");
    }

    // run
    void MoveRight() {
        // face right
        animator.transform.localScale = new Vector3(-spriteScale, spriteScale, spriteScale);
        dir = 'r';
        // move right
        gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
        animator.SetInteger("AnimState", 2);
    }

    void MoveLeft() {
        // face left
        animator.transform.localScale = new Vector3(spriteScale, spriteScale, spriteScale);
        dir = 'l';
        // move left
        gameObject.transform.Translate(-speed * Time.deltaTime, 0, 0);
        animator.SetInteger("AnimState", 2);
    }

    void Patrol() {
        bool withinLeftPatrolBoundary = transform.position.x - originalPosition.x > -patrolRange;
        bool withinRightPatrolBoundary = transform.position.x - originalPosition.x < patrolRange;
        if (!withinLeftPatrolBoundary) {
            // exceed left boundary
            MoveRight();
        } else if (!withinRightPatrolBoundary) {
            // exceed right boundary
            MoveLeft();
        } else {
            // within boundary, move in current dir
            // if blocked, change dir
            if (dir == 'l') {
                MoveLeft();
            } else if (dir == 'r') {
                MoveRight();
            }
        }
    }

    public void LockMovement()
    {
        animator.SetInteger("AnimState", 0);
        if (speed != 0) {
            workspace = speed;
        }
        this.speed = 0;
    }

    public void UnlockMovement()
    {
        speed = workspace;
    }
    protected void RandomPatrol() {
        bool withinPatrolRange = Vector3.Distance(gameObject.transform.position, originalPosition) <= patrolRange;
        if (stepLen <= 0) {
            stepFinished = true;
        }

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= huntRange) {
            // if player too close, chase player
            patrolDir = Vector3.Normalize(player.transform.position - gameObject.transform.position);
            dirUpdate();
        } else if (!withinPatrolRange) {
            // out of range: move back towards original pos
            patrolDir = Vector3.Normalize(originalPosition - gameObject.transform.position);
            dirUpdate();
            stepLen = patrolRange / 2;
            stepFinished = false;
        } 
        
        if (!stepFinished) {
            // have not finished cur step
            gameObject.transform.position += patrolDir * Time.deltaTime * speed;
            animator.SetInteger("AnimState", 2);
            stepLen -= Time.deltaTime * speed;
        } else {
            // have finished cur step
            // get a new dir
            patrolDir = Vector3.Normalize(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0));

            // change sprite dir accordingly
            dirUpdate();

            // get a new step length
            stepLen = Random.Range(patrolRange / 2.5f, patrolRange / 1.2f);

            stepFinished = false;
        }
    }

    protected void dirUpdate() {
        // whenever dir changes, need update to correct sprite dir
        if (patrolDir.x < 0) {
            // facing left
            gameObject.transform.localScale = new Vector3(spriteScale, spriteScale, spriteScale);
        } else {
            gameObject.transform.localScale = new Vector3(-spriteScale, spriteScale, spriteScale);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) {
            // go opposite if into a wall
            patrolDir *= -1;
            dirUpdate();
        }
    }

    private void ChangeDir() {
        if (dir == 'l') {
            MoveRight();
        } else if (dir == 'r') {
            MoveLeft();
        }
    }
}
