using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthBar;
    public float offset;
    private float maxHealth;
    private float health;
    private float defense;
    private Enemy enemy;
    void Start()
    {
        maxHealth = 100.0f;
        // defence is a percentage, "20" means 20% of attacker's attack point is fended off
        // health = maxHealth;
        health = maxHealth;
        defense = 20.0f;
        enemy = gameObject.GetComponent<Enemy>();
        // health bar slightly above enemy sprite
        healthBar.transform.position = gameObject.transform.position + new Vector3(0, offset, 0);
    }

    public void HealthBarUpdate() {
        // health bar bound with health points
        Debug.Log(health / maxHealth);
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }

    public void TakeDamage(float attack) {
        // attack value will be sent by the dealer
        if (!IsDead()) {
            // take damage animation
            enemy.Injure();
            // health decreases
            if (defense > 99.0f) {
            // defence capped at 99;
                defense = 99.0f;
            }
            float finalDamage = attack * (100.0f - defense) / 100.0f;
            health -= finalDamage;
            Debug.Log(health);
            // health bar decreases
            HealthBarUpdate();
        }        
    }

    public bool IsDead() {
        if (health <= 0) {
            return true;
        } else {
            return false;
        }
    }
}