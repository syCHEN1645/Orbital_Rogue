using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PermanentBuff: Buff
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // Buff disappears upon collection
            gameObject.SetActive(false);
            BuffEffect(player);
            // Remove buff from game
            Destroy(gameObject);
        }
    }

    protected abstract void BuffEffect(Player player);
}
