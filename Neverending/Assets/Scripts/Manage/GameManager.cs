using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public CinemachineVirtualCamera virtualCamera;
    public Player player;
    public List<GameObject> enemies;
    public RoomFirstGenerator generator;
    public VictoryPoint portal;
    public static int level, killCount;
    // need to get all keys to enter portal
    // 1 key in 1 room
    public GameObject keys;
    public GameObject symbol;
    public TextMeshProUGUI levelText, killCountText;
    public static List<GameObject> keySymbols;
    public static int keyCount;
    private int keyTotal;
    [SerializeField]
    private static Color bright = new Color(1, 1, 1), 
    dark = new Color(100f / 255f, 100f / 255f, 100f / 255f);

    void Start()
    {
        LoadGame();

        // get a random visualiser
        generator.visualiser = Instantiate(PGPararmeters.visualisers[Random.Range(0, 4)], Vector3.zero, Quaternion.identity);
        if (generator != null && generator.visualiser != null) {
            generator.GenerateMap(true);
        }

        keySymbols = new List<GameObject>();
        // increase level by 1 at the start of the level
        level += 1;
        // generate Map, enemies, interactables and Player
        generator.GenerateMap(true);

        // map has been generated
        keyCount = 0;
        keyTotal = generator.GetRoomCount();
        // keyCount = keyTotal;
        for (int i = 0; i < keyTotal; i++) {
            Debug.Log("key " + i);
            // instantiate a number of key symbols
            GameObject keySymbol = Instantiate(symbol, keys.transform.position + new Vector3(50 * i, 0, -1), Quaternion.identity);
            // key as a child of keys
            keySymbol.transform.SetParent(keys.transform);
            keySymbols.Add(keySymbol);
            // set to dark colour
            Debug.Log(keySymbol.GetComponent<Image>().color);
            keySymbol.GetComponent<Image>().color = dark;
            Debug.Log(keySymbol.GetComponent<Image>().color);
        }

        // find and assign gameobjects
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            enemies.Add(enemy);
            Debug.Log(enemy);
        }

        // camera follows Player
        // Debug.Log(player == null);
        // virtualCamera.LookAt = player.transform;
        // virtualCamera.Follow = player.transform;

        // set portal
        portal = FindObjectOfType<VictoryPoint>();

        // set text
        killCountText.text = "Kill Count: " + killCount;
        levelText.text = "Level: " + level;
    }

    void Update() {
        // update kill
        killCountText.text = "Kill Count: " + killCount;

        // if player is at victory point, and key F is pressed, and all keys are found
        if (player.playerData.IsDead()) {
            // if player is dead, end game
            SceneManager.LoadScene(ManagerParameters.END_SCENE);
        }
        if (portal.atVictoryPoint && Input.GetKeyDown(KeyCode.F) && keyCount >= keyTotal) {
            // press F to enter portal
            // if (level == ManagerParameters.MAX_LEVEL) {
            //     // this level is the last level, player wins, game ends
            //     // reset level to 0
            //     PlayerPrefs.SetInt(ManagerParameters.LEVEL, 0);
            //     PlayerWins();
            // } else {
            //     // not the last level, go to transition scene and then next level
            //     SaveGame();
            //     SceneManager.LoadScene(ManagerParameters.TRANSITION_SCENE);
            // }
            SaveGame();
            SceneManager.LoadScene(ManagerParameters.TRANSITION_SCENE);
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

        if (PlayerPrefs.HasKey(ManagerParameters.LEVEL)) {
            // if there is a saved kill count
            killCount = PlayerPrefs.GetInt(ManagerParameters.KILL_COUNT);
        } else {
            // if there is no saved kill count
            PlayerPrefs.SetInt(ManagerParameters.KILL_COUNT, 0);
        }
    }

    private void SaveGame()
    {
        // level
        // save current level
        PlayerPrefs.SetInt(ManagerParameters.LEVEL, level);
        // if current level breaks record
        if (!PlayerPrefs.HasKey(ManagerParameters.LEVEL_RECORD) || 
            level >= PlayerPrefs.GetInt(ManagerParameters.LEVEL_RECORD)) {
            // if there is no record, or current level >= record level, record current level
            PlayerPrefs.SetInt(ManagerParameters.LEVEL_RECORD, level);
        }
        
        // kill count
        PlayerPrefs.SetInt(ManagerParameters.KILL_COUNT, killCount);
        if (!PlayerPrefs.HasKey(ManagerParameters.KILL_RECORD) || 
            killCount >= PlayerPrefs.GetInt(ManagerParameters.KILL_RECORD)) {
            // if there is no record, or current level >= record level, record current level
            PlayerPrefs.SetInt(ManagerParameters.KILL_RECORD, killCount);
        }
    }

    private void PlayerWins()
    {
        SceneManager.LoadScene(ManagerParameters.MENU_SCENE);
        Debug.Log("You Win!");
    }

    public int GetKeyTotal() {
        return keyTotal;
    }

    public static void PickKey() {
        // key symbol changes to a bright colour
        if (keyCount < keySymbols.Count) {
            keySymbols[keyCount].GetComponent<Image>().color = bright;
            keyCount++;
        }
    }
}
