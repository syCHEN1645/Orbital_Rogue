using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    public Vector2 boxSize;
    public float castdistance;
    public LayerMask groundLayer;
    // Start is called before the first frame update

    private void OnDrawGizmos() {
        // visualise the box
        Gizmos.DrawWireCube(transform.position - transform.up * castdistance, boxSize);
    }    
}
