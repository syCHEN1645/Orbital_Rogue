using UnityEngine;
using System.Collections;

public class EnemyGroundSensor : EnemySensor {
    public bool IsGrounded() {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castdistance, layer)) {
            // check if bottom of box touches ground
            return true;
        } else {
            return false;
        }
    }
}
