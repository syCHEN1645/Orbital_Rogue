using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private float damage;
    private PlayerData playerData;
    
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerData = other.gameObject.GetComponent<PlayerData>();
            // Buff disappears upon collection
            Debug.Log("Deal Damage");
            playerData?.TakeDamage(damage);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collider = other.gameObject;
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        if (collider.CompareTag("Player")) {
            Debug.Log("Deal Damage");
            PlayerData playerData = collider.GetComponent<PlayerData>();
        }
    }*/

    public void AnimationFinishedTrigger()
    {
        Destroy(gameObject);
    }

    public void InflictDamageTrigger()
    {
        Collider2D collider = this.GetComponent<Collider2D>();
        collider.enabled = true;
    }
}
