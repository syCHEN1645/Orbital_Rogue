using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private float damage;
    private PlayerData playerData;
    private Collider2D spellCollider;

    private void Start()
    {   
        spellCollider = this.GetComponent<Collider2D>();
        spellCollider.enabled = false;
    }
    
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerData = other.gameObject.GetComponent<PlayerData>();
            playerData?.TakeDamage(damage);
        }
    }

    public void AnimationFinishedTrigger()
    {
        Destroy(gameObject);
    }

    public void InflictDamageTrigger()
    {
        spellCollider.enabled = true;
    }
}
