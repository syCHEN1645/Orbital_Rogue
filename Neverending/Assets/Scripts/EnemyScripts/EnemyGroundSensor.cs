using UnityEngine;
using System.Collections;

public class EnemyGroundSensor : MonoBehaviour {
    public Vector2 boxSize;
    public float castdistance;
    public LayerMask groundLayer;

    public bool IsGrounded() {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castdistance, groundLayer)) {
            // check if bottom of box touches ground
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
