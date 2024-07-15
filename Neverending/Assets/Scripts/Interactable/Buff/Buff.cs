using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    [SerializeField]
    protected Player player;
    [SerializeField]
    protected GameObject art;
    [SerializeField]
    protected Collider2D collide;
    protected PlayerData playerData;
    void Start() {
        playerData = player.playerData;
        collide = GetComponent<Collider2D>();
    }
}
