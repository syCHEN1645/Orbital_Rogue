using UnityEngine;
using System.Collections;

public class EnemyLeftSensor : EnemySensor {
    public bool IsBlocked() {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.right, castdistance, layer)) {
            // check if left side of box touches ground
            return true;
        } else {
            return false;
        }
    }
    // note: when sprite flips, sensor also flips
}
