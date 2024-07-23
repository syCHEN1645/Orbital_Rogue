using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalKey : MonoBehaviour
{
    protected Trajectory traj;

    void Start() {
        traj = GetComponent<Trajectory>();
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            GameManager.keyCount ++;
            // can add some buff also
            Debug.Log(GameManager.keyCount);
            Destroy(gameObject);
            // gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Ground")) {
            // if bouncing out of map
            if (traj != null) {
                traj.dir *= -1;
            }
        }
    }
}
