using UnityEngine;
using System.Collections;

public class EnemyRightSensor : EnemySensor {
    public bool IsBlockedRight() {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, transform.right, castdistance, groundLayer)) {
            // check if right side of box touches ground
            Debug.Log("right");
            return true;
        } else {
            return false;
        }
    }
}
