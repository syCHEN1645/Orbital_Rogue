using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField]
    protected GameObject obj; 
    [SerializeField]
    protected float coe, speed, x, drag, absorb;
    protected Vector3 originalPos;
    public float dir;

    // Start is called before the first frame update
    void Start()
    {
        // coefficient for formula
        coe = Random.Range(2.0f, 3.0f);
        // speed of x component
        speed = 2.0f;
        x = 0;
        // gravity drag
        drag = 4.0f;
        // bounce energy absorb
        absorb = 1.4f;
        originalPos = obj.transform.position;
        // dir == -1: bounce to left
        // dir == 1: bounce to right
        // 50% chance
        if (Random.Range(0, 1.0f) < 0.5f) {
            dir = 1;
        } else {
            dir = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Bounce();
        if (coe < 0.2f) {
            // stop bouncing
            gameObject.SetActive(false);
        }
    }

    protected void Bounce() {
        x += dir * speed * Time.deltaTime;
        obj.transform.position = originalPos + TrajectoryFormula(coe, x);
        if (obj.transform.position.y <= originalPos.y) {
            // now obj touches the "ground", next bounce weaker
            // upfate originalPos, reset x
            originalPos = obj.transform.position;
            coe /= absorb;
            x = 0;
        }
    }

    protected Vector3 TrajectoryFormula(float coe, float x) {
        float y;
        // formula
        y = -x * x + dir * coe * x - drag * x * x;
        return new Vector3(x, y, 0);
    }
}
