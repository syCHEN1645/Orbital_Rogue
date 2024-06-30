using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public CinemachineVirtualCamera virtualCamera;
    public GameObject player;
    public List<GameObject> enemies;
    public AbstractGenerator generator;
    public VictoryPoint portal;
    public static int level = 0;

    void Start()
    {
        // increase level by 1 at the start of the level
        level += 1;
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

        // set portal
        portal = GameObject.FindObjectOfType<VictoryPoint>();
    }

    void Update() {
        // if player is at victory point, and key F is pressed
        if (portal.atVictoryPoint && Input.GetKeyDown(KeyCode.F)) {
            // press F to enter portal
            if (level == ManagerParameters.MAX_LEVEL) {
                // this level is the last level, player wins, game ends
                PlayerWins();
            } else {
                // not the last level, go to transition scene and then next level
                SceneManager.LoadScene(ManagerParameters.TRANSITION_SCENE);
            }
        }
    }

    private void PlayerWins()
    {
        throw new NotImplementedException();
    }
}
