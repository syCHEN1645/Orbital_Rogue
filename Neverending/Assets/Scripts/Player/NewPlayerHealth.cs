using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private TextMeshProUGUI healthPoint;
    [SerializeField]
    private float health, maxHealth, defense;

    void Start()
    {
        maxHealth = 100.0f;
        health = maxHealth;
        // defence is a percentage, "20" means 20% of attacker's attack point is fended off
        defense = 20.0f;
        HealthBarUpdate();
    }

    private void HealthBarUpdate() {
        // health bar updates
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        // health point text updates
        healthPoint.text = health + " / " + maxHealth;
    }

    public void TakeDamage(float attack) {
        // attack value will be sent by the dealer

        // if (defense > 99.0f) {
        //     // defence capped at 99;
        //     defense = 99.0f;
        // }
        
        if (!IsDead()) {
            // take damage animation
            // health point decreases
            float finalDamage = attack * (100.0f - defense) / 100.0f;
            health -= finalDamage;
        }
        // update health bar accordingly
        HealthBarUpdate();
    }

    public bool IsDead() {
        if (health <= 0.0f) {
            return true;
        } 
        return false;
    }

    public void RecoverHealth(float amt) {
        health += amt;
        HealthBarUpdate();
    }

    public void IncreaseDefense(float amt) {
        defense += amt;
    }

    public void IncreaseMaxHealth(float amt) {
        maxHealth += amt;
        health += amt;
        HealthBarUpdate();
    }
}
