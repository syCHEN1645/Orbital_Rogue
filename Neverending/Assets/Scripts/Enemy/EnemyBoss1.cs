using UnityEngine;

public class EnemyBoss1 : Enemy
{
    [SerializeField]
    // enemy stops at a distance of stopMovingDistance away from Player
    // enemy starts hunting if Player is closer than huntRange
    protected float healthBarOffset, stopMovingDistance, huntRange;
    [SerializeField]
    protected float meleeAttack, rangedAttack;
    void Start()
    {
        InitialiseEnemy();
    }

    void Update()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= huntRange) {
            HuntPlayer();
        } else {
            // idle around
        }
    }

    protected override void InitialiseEnemy()
    {
        base.InitialiseEnemy();
        speed = 2.0f;
        attack = 15.0f;
        attackRange = 4.0f;
        attackInterval = 1.0f;
        spriteScale = 1.0f;

        healthBarOffset = 1.4f;
        stopMovingDistance = 0.5f;
        huntRange = 15.0f;
        meleeAttack = 60.0f;
        rangedAttack = 40.0f;

        enemyHealth.SetDefense(30);
        enemyHealth.SetMaxHealth(100);
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
        animator.SetTrigger("Walk");
    }
    public override void Injure() {
        animator.SetTrigger("Hurt");
    }

    protected bool WithinHuntRange() {
        return true;
    }

    protected void HuntPlayer()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) > stopMovingDistance) {
            MoveTowardsPlayer();
        }
        if (WithinAttackRange()) {
            Debug.Log("Attack");
            StartCoroutine(AttackPlayer());
        }
    }

    protected void MoveTowardsPlayer()
    {
        var unitVector = GetUnitVectorTowardsPlayer();
        gameObject.transform.Translate(
            speed * Time.deltaTime * unitVector.x, 
            speed * Time.deltaTime * unitVector.y, 
            0);
    }

    protected Vector3 GetUnitVectorTowardsPlayer() {
        // x = x, y = y, z = 0
        return Vector3.Normalize(player.transform.position - gameObject.transform.position);
    }

    protected void Attack1() {
        // melee attack
        Attack();
        
    }

    protected void Attack2() {
        // ranged attack
        Cast();
    }

    // protected void Attack3() {
    //     // melee attack
    //     Attack();
    // }

    // void MoveRight() {
    //     // face right
    //     animator.transform.localScale = new Vector3(-spriteScale, spriteScale, spriteScale);
    //     dir = 'r';
    //     // move right
    //     gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
    //     animator.SetInteger("AnimState", 2);
    // }

    // void MoveLeft() {
    //     // face left
    //     animator.transform.localScale = new Vector3(spriteScale, spriteScale, spriteScale);
    //     dir = 'l';
    //     // move left
    //     gameObject.transform.Translate(-speed * Time.deltaTime, 0, 0);
    //     animator.SetInteger("AnimState", 2);
    // }
    // private void ChangeDir() {
    //     if (dir == 'l') {
    //         MoveRight();
    //     } else if (dir == 'r') {
    //         MoveLeft();
    //     }
    // }
}
