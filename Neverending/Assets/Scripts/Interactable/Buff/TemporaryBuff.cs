using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TemporaryBuff : MonoBehaviour
{
    [SerializeField] protected Collider2D collide;
    [SerializeField] protected float duration;
    [SerializeField] protected GameObject art;
    private PlayerData playerData;

    void Start()
    {
        collide = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            playerData = other.gameObject.GetComponent<PlayerData>();
            // Buff disappears upon collection
            art.SetActive(false);
            // Disable Collider2D
            collide.enabled = false;
            StartCoroutine(BuffEffect(playerData));
        }
    }
    protected abstract IEnumerator BuffEffect(PlayerData playerData);
    protected abstract void RemoveBuffEffect(PlayerData playerData);
}