using UnityEngine;

public abstract class PermanentBuff : Buff
{
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // playerData = other.gameObject.GetComponent<PlayerData>();
            // Buff disappears upon collection
            gameObject.SetActive(false);
            BuffEffect(playerData);
            // Remove buff from game
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Ground")) {
            // if bouncing out of map
            if (traj != null) {
                traj.dir *= -1;
            }
        }
    }

    protected abstract void BuffEffect(PlayerData playerData);
}
