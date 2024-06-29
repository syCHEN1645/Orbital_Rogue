using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public CinemachineVirtualCamera virtualCamera;
    public GameObject player;
    public List<GameObject> enemies;
    public AbstractGenerator generator;

    void Start()
    {
        // generate Map, enemies, interactables and Player
        generator.GenerateMap(true);

        // find and assign gameobjects
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            enemies.Add(enemy);
            Debug.Log(enemy);
        }

        // camera follows Player
        // Debug.Log(player == null);
        virtualCamera.LookAt = player.transform;
        virtualCamera.Follow = player.transform;
        
    }
}
