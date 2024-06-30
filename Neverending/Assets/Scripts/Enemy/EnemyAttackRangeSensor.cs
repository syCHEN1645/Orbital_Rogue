using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRangeSensor : EnemySensor
{
    public bool IsInRange() {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.right, castdistance, layer)) {
            // check if left side of box touches ground
            return true;
        } else {
            return false;
        }
    }
}
