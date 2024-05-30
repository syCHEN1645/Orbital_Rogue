using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLight : MonoBehaviour
{
    public Animator animator;
    public EnemyGroundSensor groundSensor;
    public EnemyLeftSensor leftSensor;
    private Rigidbody2D rb;
    private Collider2D collider2d;
    private Vector2 originalPosition;
    private GameObject player;
    private EnemyHealth enemyHealth;
    private float patrolRange = 10.0f;
    private float speed = 1.0f;
    // dir: enemy is facing this direction: l-left, r-right.
    private char dir;
    // hitWall: banging into a wall
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        collider2d = GetComponent<Collider2D>();
        player = GameObject.Find("Player");
        enemyHealth = gameObject.GetComponent<EnemyHealth>();
        originalPosition = transform.position;
        // m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        // set initial dir being left
        animator.transform.localScale = new Vector3(-1, 1, 1);
        dir = 'l';
        // set health, attack, defence
        // hitpoint = attacker's attack * defender's defence
        // health -= hitpoint
        // health is managed in HealthBar
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (enemyHealth.IsDead()) {
            Die();        
            StartCoroutine(bodyDisappear());
        } else {
            Patrol();
        }
    }


    public void Attack() {
        animator.SetTrigger("attack");
    }

    public void Injure() {
        animator.SetTrigger("Hurt");
    }

    public void Die() {
        // animate death
        animator.SetTrigger("Death");
    }

    private IEnumerator bodyDisappear() {
        // wait for 2 seconds then body will disappear.
        yield return new WaitForSeconds(2);
        // remove sprite
        Destroy(gameObject);
    }

    // run
    void MoveRight() {
        // face right
        animator.transform.localScale = new Vector3(-1, 1, 1);
        dir = 'r';
        // move right
        gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
        animator.SetInteger("AnimState", 2);
    }

    void MoveLeft() {
        // face left
        animator.transform.localScale = new Vector3(1, 1, 1);
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
            if (leftSensor.IsBlocked()) {
                ChangeDir();
            } else {
                if (dir == 'l') {
                    MoveLeft();
                } else if (dir == 'r') {
                    MoveRight();
                }
            }
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
