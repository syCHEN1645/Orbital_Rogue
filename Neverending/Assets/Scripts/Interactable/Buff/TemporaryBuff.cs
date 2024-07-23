using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TemporaryBuff : Buff
{
    [SerializeField] protected float duration;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerData = other.gameObject.GetComponent<PlayerData>();
            // Buff disappears upon collection
            art.SetActive(false);
            // Disable Collider2D
            collide.enabled = false;
            StartCoroutine(BuffEffect(playerData));
        }

        if (other.gameObject.CompareTag("Ground")) {
            // if bouncing out of map
            if (traj != null) {
                traj.dir *= -1;
            }
        }
    }
    protected abstract IEnumerator BuffEffect(PlayerData playerData);
    protected abstract void RemoveBuffEffect(PlayerData playerData);
}