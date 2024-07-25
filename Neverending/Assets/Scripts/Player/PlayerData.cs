using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthPoint;
    [field: SerializeField] public float MovementVelocity { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float Defense { get; private set; }
    [field: SerializeField] public float Attack { get; private set; }
    [field: SerializeField] public float StunTime { get; private set; }
    [field: SerializeField] public float DashTime { get; private set; }
    [field: SerializeField] public float DashVelocity { get; private set; }
    [field: SerializeField] public float DashCooldown { get; private set; }
    [field: SerializeField] public float MaxHoldTime { get; private set; }
    [field: SerializeField] public float HoldTimeScale { get; private set; } 
    [field: SerializeField] public float Drag { get; private set; }
    [field: SerializeField] public float DashEndYMultiplier { get; private set; }
    [field: SerializeField] public float DistBetweenAfterImages { get; private set; }
    [SerializeField] private float health;
    [SerializeField] private float stunInterval = 0.5f;
    private Player player;
    private float workspace;
    private bool canBeStunned;

    public bool isSLowed { get; private set; }

    void Start()
    {
        health = MaxHealth;
        if (healthBar != null) {
            HealthBarUpdate();
        }
        player = gameObject.GetComponent<Player>();
        canBeStunned = true;
    }

    public void HealthBarUpdate() {
        // health bar changes
        healthBar.fillAmount = Mathf.Clamp(health / MaxHealth, 0, 1);
        // health point changes
        healthPoint.text = "" + health;
        Debug.Log("healthbar updated");
    }

    public IEnumerator TemporarySlow(float value, float duration) {
        float temp = MovementVelocity;
        MovementVelocity = value;
        yield return new WaitForSeconds(duration);
        MovementVelocity = temp;
        isSLowed = true;
    }

    public void Slow (float value) 
    {
        if (MovementVelocity != 0) {
            workspace = MovementVelocity;
        }
        MovementVelocity = value;
        isSLowed = true;
    }

    public void RecoverSpeed()
    {
        MovementVelocity = workspace;
        isSLowed = false;
    }

    public void TakeDamage(float damage) {
        Debug.Log("player take damage: " + damage);
        // attack value will be sent by the dealer
        if (canBeStunned) {
            player.StateMachine.ChangeState(player.StunState);
            StartCoroutine(StunCoolDown());
        }
        
        if (!IsDead()) {
            // take damage animation
            // health decreases
            float finalDamage = damage * (100.0f - Defense) / 100.0f;
            health -= finalDamage;
            // health bar decreases
            HealthBarUpdate();
        }        
    }

    private IEnumerator StunCoolDown()
    {
        canBeStunned = false;
        yield return new WaitForSeconds(stunInterval);
        canBeStunned = true;
    }

    public bool IsDead() {
        if (health <= 0) {
            return true;
        } else {
            return false;
        }
    }

    public void AttackBoost(float amt) {
        Attack += amt;
    }

    public void RecoverAttack(float amt) {
        Attack -= amt;
    }

    public void IncreaseDefense(float amt) {
        Defense += amt;
    }

    public void RecoverHealth(float amt) {
        health += amt;
        HealthBarUpdate();
    }

    public void IncreaseMaxHealth(float amt) {
        MaxHealth += amt;
        health += amt;
        HealthBarUpdate();
    }

    public float GetMaxHealth() {
        return maxHealth;
    }

    public float GetDefense() {
        return defense;
    }

    public float GetAttack() {
        return Damage;
    }
    public float GetHealth() {
        return health;
    }

    public void SetMaxHealth(float val) {
        this.maxHealth = val;
    }
    public void SetDefense(float val) {
        this.defense = val;
    }
    public void SetAttack(float val) {
        this.Damage = val;
    }
    public void SetHealth(float val) {
        this.health = val;
    }
}
