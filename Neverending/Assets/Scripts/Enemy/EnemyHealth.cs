using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    
    public Image healthBar;
    // defence is a percentage, "20" means 20% of attacker's attack point is fended off
    [SerializeField] private float maxHealth, health, defense, offset;
    private Enemy enemy;
    void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    public void HealthBarUpdate() {
        if (health > maxHealth) {
            health = maxHealth;
        }
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

    public float GetOffset() {
        return offset;
    }

    public void SetDefense(float val) {
        this.defense = val;
    }

    public void SetMaxHealth(float val) {
        this.maxHealth = val;
    }

    public void SetHealth(float val) {
        this.health = val;
    }
}
