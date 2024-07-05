using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    protected Player player;
    [SerializeField]
    protected GameObject art;
    [SerializeField]
    protected Collider2D collide;
    void Start() {
        player = FindObjectOfType<Player>();
        collide = GetComponent<Collider2D>();
    }
}
