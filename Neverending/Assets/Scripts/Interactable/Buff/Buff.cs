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
    [SerializeField]
    protected Trajectory traj;
    void Start() {
        player = FindObjectOfType<Player>();
        playerData = player.playerData;
        collide = GetComponent<Collider2D>();
        traj = GetComponent<Trajectory>();
    }
}
