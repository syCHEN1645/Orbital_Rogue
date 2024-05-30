using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlueSlime : MonoBehaviour
{
    private EnemyBlueSlimeHealth blueSlimeHealth;
    // Start is called before the first frame update
    void Start()
    {
        blueSlimeHealth = gameObject.GetComponent<EnemyBlueSlimeHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        if (blueSlimeHealth.IsDead()) {
            Die();
        }
    }

    void OnCollisionEnter(Collision other) {
        // if collide with player
        if (other.gameObject.CompareTag("Player")) {
            // deal damage to player
            // other.gameObject.TakeDamage();
        }
    }

    private void Die() {
        StartCoroutine("BodyDisappear");
    }

    private IEnumerator BodyDisappear() {
        // wait for 2 seconds then body will disappear.
        yield return new WaitForSeconds(2);
        // remove sprite
        Destroy(gameObject);
    }
}
