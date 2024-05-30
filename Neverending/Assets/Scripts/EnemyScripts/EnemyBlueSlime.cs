using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlueSlime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // collider = gameObject.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        // if collide with player
        if (other.gameObject.CompareTag("Player")) {
            // deal damage to player
            // other.gameObject.TakeDamage();
        }
    }
}
