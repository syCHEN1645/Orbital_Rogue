using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthBar;
    public float offset = 1.0f;
    private float maxHealth;
    private float health;
    private float defense;
    private Enemy enemy;
    void Start()
    {
        maxHealth = 10.0f;
        // defence is a percentage, "20" means 20% of attacker's attack point is fended off
        // health = maxHealth;
        health = maxHealth;
        defense = 20.0f;
        enemy = gameObject.GetComponent<Enemy>();
        // health bar slightly above enemy sprite
        // healthBar.transform.position = enemy.transform.position + new Vector3(0, offset, 0);
        // Debug.Log(healthBar.transform.position);
    }

    public void HealthBarUpdate() {
        // health bar bound with health points
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }

    public void TakeDamage(float attack) {
        // attack value will be sent by the dealer
        if (defense > 99.0f) {
            // defence capped at 99;
            defense = 99.0f;
        }
        if (!IsDead()) {
            // take damage animation
            enemy.Injure();
            // health decreases
            float finalDamage = attack * (100.0f - defense) / 100.0f;
            health -= finalDamage;
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
