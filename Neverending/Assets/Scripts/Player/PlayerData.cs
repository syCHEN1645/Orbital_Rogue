using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    public float MovementVelocity = 10f;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private TextMeshProUGUI healthPoint;
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float defense;
    [SerializeField]
    public float Damage { get; private set; }
    private float workspace;

    void Start()
    {
        health = maxHealth;
        defense = 20.0f;
        Damage = 5f;
        if (healthBar != null) {
            HealthBarUpdate();
        }
    }

    public void HealthBarUpdate() {
        // health bar changes
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        // health point changes
        healthPoint.text = "" + health;
        Debug.Log("healthbar updated");
    }

    public IEnumerator TemporarySlow(float value, float duration) {
        float temp = MovementVelocity;
        MovementVelocity = value;
        yield return new WaitForSeconds(duration);
        MovementVelocity = temp;
    }

    public void Slow (float value) 
    {
        if (MovementVelocity != 0) {
            workspace = MovementVelocity;
        }
        MovementVelocity = value;
    }

    public void RecoverSpeed()
    {
        MovementVelocity = workspace;
    }

    public void TakeDamage(float attack) {
        // Debug.Log("player movement speed" + MovementVelocity);
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

    public void AttackBoost(float amt) {
        Damage += amt;
    }

    public void RecoverAttack(float amt) {
        Damage -= amt;
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
