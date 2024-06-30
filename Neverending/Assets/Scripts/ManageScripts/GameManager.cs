using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public CinemachineVirtualCamera virtualCamera;
    public GameObject player;
    public List<GameObject> enemies;
    public AbstractGenerator generator;
    public VictoryPoint portal;
    public int level = 0;

    void Start()
    {
        LoadGame();
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
                // reset level to 0
                PlayerPrefs.SetInt(ManagerParameters.LEVEL, 0);
                PlayerWins();
            } else {
                // not the last level, go to transition scene and then next level
                SaveGame();
                SceneManager.LoadScene(ManagerParameters.TRANSITION_SCENE);
            }
        }
    }

    private void LoadGame() {
        if (PlayerPrefs.HasKey(ManagerParameters.LEVEL)) {
            // if there is a saved level
            level = PlayerPrefs.GetInt(ManagerParameters.LEVEL);
        } else {
            // if there is no saved level
            PlayerPrefs.SetInt(ManagerParameters.LEVEL, 0);
        }
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt(ManagerParameters.LEVEL, level);
    }

    private void PlayerWins()
    {
        SceneManager.LoadScene(ManagerParameters.MENU_SCENE);
        Debug.Log("You Win!");
    }
}
