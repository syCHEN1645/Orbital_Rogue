using UnityEngine;
using System.Collections;

public class EnemyRightSensor : MonoBehaviour {
    public Vector2 boxSize;
    public float castdistance;
    public LayerMask groundLayer;

    public bool IsBlockedRight() {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, transform.right, castdistance, groundLayer)) {
            // check if right side of box touches ground
            Debug.Log("right");
            return true;
        } else {
            return false;
        }
    }
    private void OnDrawGizmos() {
        // visualise the box
        Gizmos.DrawWireCube(transform.position - transform.up * castdistance, boxSize);
    }
}
