using UnityEngine;

public class EnemyBoss1 : Enemy
{
    [SerializeField]
    // enemy stops at a distance of stopMovingDistance away from Player
    protected float healthBarOffset, stopMovingDistance;
    void Start()
    {
        InitialiseEnemy();
    }

    void Update()
    {
        HuntPlayer();
        
    }

    protected override void InitialiseEnemy()
    {
        base.InitialiseEnemy();
        speed = 0.7f;
        attack = 15.0f;
        attackRange = 4.0f;
        attackInterval = 1.0f;
        spriteScale = 1.0f;
        healthBarOffset = 1.4f;
        stopMovingDistance = 0.5f;
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
    public override void Die() {
        animator.SetTrigger("Death");
    }
    protected void Walk() {
        animator.SetTrigger("Walk");
    }
    protected void Hurt() {
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
