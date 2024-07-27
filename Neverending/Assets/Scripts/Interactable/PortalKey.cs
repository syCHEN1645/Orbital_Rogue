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
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("check");
            GameManager.PickKey();
            // can add some buff also
            Debug.Log(GameManager.keyCount);
            Destroy(gameObject);
            // gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Ground")) {
            Debug.Log("ground");
            // if bouncing out of map
            if (traj != null) {
                traj.dir *= -1;
            }
        }
    }
}
