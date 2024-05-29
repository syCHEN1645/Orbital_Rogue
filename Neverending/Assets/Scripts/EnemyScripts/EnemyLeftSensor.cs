using UnityEngine;
using System.Collections;

public class EnemyLeftSensor : MonoBehaviour {
    public Vector2 boxSize;
    public float castdistance;
    public LayerMask groundLayer;

    public bool IsBlocked() {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.right, castdistance, groundLayer)) {
            // check if left side of box touches ground
            return true;
        } else {
            return false;
        }
    }
    private void OnDrawGizmos() {
        // visualise the box
        Gizmos.DrawWireCube(transform.position - transform.up * castdistance, boxSize);
    }

    // note: when sprite flips, sensor also flips
}
