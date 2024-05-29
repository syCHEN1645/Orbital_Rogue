using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public EnemyGroundSensor groundSensor;
    public EnemyLeftSensor leftSensor;
    private Rigidbody2D rb;
    private Collider2D collider2d;
    private Vector2 originalPosition;
    private GameObject player;
    private float patrolRange = 10.0f;
    private float speed = 1.0f;
    // dir: enemy is facing this direction: l-left, r-right.
    private char dir;
    // hitWall: banging into a wall
    private float health;
    private float attack;
    private float defence;
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        collider2d = GetComponent<Collider2D>();
        player = GameObject.Find("Player");
        originalPosition = transform.position;
        // m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        // set initial dir being left
        animator.transform.localScale = new Vector3(-1, 1, 1);
        dir = 'l';
        // set health, attack, defence
        // hitpoint = attacker's attack * defender's defence
        // health -= hitpoint
        health = 10.0f;
        attack = 5.0f;
        defence = 0.05f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Patrol();
    }


    void Attack() {
        animator.SetTrigger("attack");
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
