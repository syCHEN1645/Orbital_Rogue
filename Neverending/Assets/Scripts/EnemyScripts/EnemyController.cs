using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;
    private Collider2D collider2d;
    private Vector2 originalPosition;
    private float patrolRange = 5.0f;
    private float speed = 1.0f;
    // dir: enemy is facing this direction: l-left, r-right.
    private char dir;

    // private Sensor_Bandit       m_groundSensor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        collider2d = GetComponent<Collider2D>();
        originalPosition = transform.position;
        // m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        // set initial dir being left
        animator.transform.localScale = new Vector3(-1, 1, 1);
        dir = 'l';
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
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
            if (dir == 'l') {
                MoveLeft();
            } else if (dir == 'r') {
                MoveRight();
            }
        }
    }
}
