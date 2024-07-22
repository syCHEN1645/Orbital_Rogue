using UnityEngine;

public abstract class PermanentBuff : MonoBehaviour
{
    private PlayerData playerData;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerData = other.gameObject.GetComponent<PlayerData>();
            // Buff disappears upon collection
            gameObject.SetActive(false);
            BuffEffect(playerData);
            // Remove buff from game
            Destroy(gameObject);
        }
    }

    protected abstract void BuffEffect(PlayerData playerData);
}
