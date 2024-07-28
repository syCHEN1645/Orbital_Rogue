using Unity.VisualScripting;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    [SerializeField]
    protected GameObject player;
    [SerializeField]
    protected GameObject art;
    [SerializeField]
    protected Collider2D collide;
    protected PlayerData playerData;
    [SerializeField]
    protected Trajectory traj;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerData = player.GetComponent<PlayerData>();
        collide = GetComponent<Collider2D>();
        traj = GetComponent<Trajectory>();
    }
}
