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
    //[field: SerializeField] public float DashTime { get; private set; }
    [field: SerializeField] public float DashVelocity { get; private set; }
    [field: SerializeField] public float DashCooldown { get; private set; }
    [SerializeField] private float health;
    [SerializeField] private float stunInterval = 0.5f;
    private Player player;
    private float workspace;
    private bool canBeStunned;
    public bool isSLowed { get; private set; }
    private bool immune = false;

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

    public void Slow(float value) 
    {
        if (MovementVelocity != 0) {
            workspace = MovementVelocity;
        }
        MovementVelocity = value;
        isSLowed = true;
    }

    public void RecoverSpeed()
    {
        Debug.Log("workspace: " + workspace);
        MovementVelocity = workspace;
        isSLowed = false;
    }

    public IEnumerator TemporarySetStunDuration(float duration)
    {
        float temp = StunTime;
        StunTime = duration;
        yield return new WaitForSeconds(duration);
        StunTime = temp;
    }

    public void TakeDamage(float damage) {
        if (!immune) {
            Debug.Log("player take damage: " + damage);
            // attack value will be sent by the dealer
            if (canBeStunned) {
                player.StateMachine.ChangeState(player.StunState);
                StartCoroutine(StunCoolDown());
            }
            // take damage animation
            // health decreases  
            // health bar decreases
            float finalDamage = damage * (100.0f - Defense) / 100.0f;
            health -= finalDamage;
            if (health <= 0) {
                player.StateMachine.ChangeState(player.DeathState);
                immune = true;
            }
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

    public void DefenseBoost(float amt) {
        Defense += amt;
    }

    public void RecoverAttack(float amt) {
        Attack -= amt;
    }

    public void RecoverDefense(float amt) {
        Defense -= amt;
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
        return MaxHealth;
    }

    public float GetDefense() {
        return Defense;
    }

    public float GetAttack() {
        return Attack;
    }
    public float GetHealth() {
        return health;
    }

    public void SetMaxHealth(float val) {
        this.MaxHealth = val;
    }
    public void SetDefense(float val) {
        this.Defense = val;
    }
    public void SetAttack(float val) {
        this.Attack= val;
    }
    public void SetHealth(float val) {
        this.health = val;
    }
}
