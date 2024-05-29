using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private const float HEALTHBAR_MAX_LENGTH = 0.5f;
    private const float maxHealth = 10.0f;
    private float health = maxHealth;
    private float defense;
    // Start is called before the first frame update
    void Start()
    {
        // defence is a percentage, "20" means 20% of attacker's attack point is fended off
        // health = maxHealth;
        defense = 20.0f;
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void TakeDamage(float attack) {
        // attack value will be sent by the dealer
        if (defense > 99.0f) {
            // defence capped at 99;
            defense = 99.0f;
        }
        health -= attack * (100.0f - defense) / 100.0f;
    }

    public bool IsDead() {
        if (health <= 0) {
            return true;
        } else {
            return false;
        }
    }
}
