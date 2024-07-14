using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NewPlayerHealth : MonoBehaviour
{
    public Image healthBar;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float health;
    [SerializeField]
    private float defense;
    void Start()
    {
        maxHealth = 100.0f;
        health = maxHealth;
        defense = 20.0f;
    }

    public void HealthBarUpdate() {
        // health bar bound with health points
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }

    public void TakeDamage(float attack) {
        // attack value will be sent by the dealer

        if (!IsDead()) {
            // take damage animation
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

    public void IncreaseDefense(float amt) {
        defense += amt;
    }

    public void RecoverHealth(float amt) {
        health += amt;
        HealthBarUpdate();
    }

    public void IncreaseMaxHealth(float amt) {
        maxHealth += amt;
        health += amt;
        HealthBarUpdate();
    }
}
