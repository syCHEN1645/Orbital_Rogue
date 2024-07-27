using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private float damage;
    private PlayerData playerData;
    private Collider2D collider;

    private void Start()
    {   
        collider = this.GetComponent<Collider2D>();
        collider.enabled = false;
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
        collider.enabled = true;
    }
}
