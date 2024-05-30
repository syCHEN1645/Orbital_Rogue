using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthBar;
    private const float MAX_HEALTH = 10.0f;
    private EnemyLight enemyLight;
    private float health;
    private float defense;
    // Start is called before the first frame update
    void Start()
    {
        // defence is a percentage, "20" means 20% of attacker's attack point is fended off
        // health = maxHealth;
        health = MAX_HEALTH;
        defense = 20.0f;
        enemyLight = gameObject.GetComponent<EnemyLight>();
    }

    // Update is called once per frame
    void Update()
    {
        // health bar bound with health points
        healthBar.fillAmount = Mathf.Clamp(health / MAX_HEALTH, 0, 1);
        // health bar follows enemy sprite
        healthBar.transform.position = gameObject.transform.position + new Vector3(0, 1.3f, 0);
    }

    public void TakeDamage(float attack) {
        // attack value will be sent by the dealer
        if (defense > 99.0f) {
            // defence capped at 99;
            defense = 99.0f;
        }
        
        if (!IsDead()) {
            // take damage animation
            enemyLight.Injure();
            // health points decrease
            float finalDamage = attack * (100.0f - defense) / 100.0f;
            health -= finalDamage;
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
