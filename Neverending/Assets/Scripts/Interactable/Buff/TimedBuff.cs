using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedBuff : Buff
{
    [SerializeField]
    protected float duration;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // Buff disappears upon collection
            art.SetActive(false);
            // Disable Collider2D
            collide.enabled = false;
            StartCoroutine(BuffEffect(player));
        }
    }
    protected abstract IEnumerator BuffEffect(Player player);
    protected abstract void RemoveBuffEffect(Player player);
}