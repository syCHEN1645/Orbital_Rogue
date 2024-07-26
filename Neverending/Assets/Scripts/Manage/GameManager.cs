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
        keyTotal = generator.GetRoomCount() - 1;
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

        // load player stats
        LoadPlayer();

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

    private void LoadPlayer() {
        // load only if this is not the 1st level
        if (level > 1) {
            player.playerData.SetAttack(PlayerPrefs.GetFloat(ManagerParameters.CURRNET_ATTACK));
            player.playerData.SetDefense(PlayerPrefs.GetFloat(ManagerParameters.CURRNET_DEFENSE));
            player.playerData.SetMaxHealth(PlayerPrefs.GetFloat(ManagerParameters.CURRNET_MAX_HEALTH));
            player.playerData.SetHealth(PlayerPrefs.GetFloat(ManagerParameters.CURRNET_MAX_HEALTH));
            player.playerData.HealthBarUpdate();
        }
    }

    private void LoadGame() {
        if (PlayerPrefs.HasKey(ManagerParameters.CURRNET_LEVEL)) {
            // if there is a saved level, load from there
            level = PlayerPrefs.GetInt(ManagerParameters.CURRNET_LEVEL);
            killCount = PlayerPrefs.GetInt(ManagerParameters.CURRNET_KILL);
        } else {
            // if there is no saved level, load 0
            PlayerPrefs.SetInt(ManagerParameters.CURRNET_LEVEL, 0);
            PlayerPrefs.SetInt(ManagerParameters.CURRNET_KILL, 0);
            for (int i = ManagerParameters.CURRENT.Length - 3; i < ManagerParameters.CURRENT.Length; i++) {
                PlayerPrefs.SetFloat(ManagerParameters.CURRENT[i], 0);
            }
            // PlayerPrefs.SetFloat(ManagerParameters.CURRNET_ATTACK, 0);
            // PlayerPrefs.SetFloat(ManagerParameters.CURRNET_DEFENSE, 0);
            // PlayerPrefs.SetFloat(ManagerParameters.CURRNET_MAX_HEALTH, 0);
            
            // this is a new game, record is not broken
            PlayerPrefs.SetInt(ManagerParameters.BREAK_LEVEL_RECORD, 0);
            PlayerPrefs.SetInt(ManagerParameters.BREAK_KILL_RECORD, 0);
        }
    }

    private void SaveGame()
    {
        // level
        // save current level
        float[] stats = {
            player.playerData.GetAttack(),
            player.playerData.GetDefense(), 
            player.playerData.GetMaxHealth()
        };
        PlayerPrefs.SetInt(ManagerParameters.CURRNET_LEVEL, level);
        PlayerPrefs.SetInt(ManagerParameters.CURRNET_KILL, killCount);
        for (int i = ManagerParameters.CURRENT.Length - 3, j = 0; i < ManagerParameters.CURRENT.Length && j < 3; i++, j++) {
            PlayerPrefs.SetFloat(ManagerParameters.CURRENT[i], stats[j]);
        }
        // PlayerPrefs.SetFloat(ManagerParameters.CURRNET_ATTACK, stats[0]);
        // PlayerPrefs.SetFloat(ManagerParameters.CURRNET_DEFENSE, stats[1]);
        // PlayerPrefs.SetFloat(ManagerParameters.CURRNET_MAX_HEALTH, stats[2]);

        // if current level breaks record
        if (!PlayerPrefs.HasKey(ManagerParameters.LEVEL_RECORD_LEVEL) || 
            level >= PlayerPrefs.GetInt(ManagerParameters.LEVEL_RECORD_LEVEL)) {
            // if there is no record, or current level >= record level, record is broken
            // 1: break
            // 0: not break
            PlayerPrefs.SetInt(ManagerParameters.BREAK_LEVEL_RECORD, 1);
        }
        
        // if current kill count breaks record
        if (!PlayerPrefs.HasKey(ManagerParameters.KILL_RECORD_KILL) || 
            killCount >= PlayerPrefs.GetInt(ManagerParameters.KILL_RECORD_KILL)) {
            // if there is no record, or current level >= record level, record is broken
            // 1: break
            // 0: not break
            PlayerPrefs.SetInt(ManagerParameters.BREAK_KILL_RECORD, 1);
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
