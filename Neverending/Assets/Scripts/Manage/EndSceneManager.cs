using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    public TextMeshProUGUI curKill, curLevel, curStats,
        levelRecordKill, levelRecordLevel, levelRecordStats, 
        killRecordKill, killRecordLevel, killRecordStats;
    public Button mainMenuButton;
    
    void Start()
    {
        // load all stats (3 sets: current, kill-record, level-record)
        LoadTexts(curKill, curLevel, curStats, ManagerParameters.CURRENT);
        LoadTexts(levelRecordKill, levelRecordLevel, levelRecordStats, ManagerParameters.LEVEL);
        LoadTexts(killRecordKill, killRecordLevel, killRecordStats, ManagerParameters.KILL);
        // if break record, mark that text (kill/level) red
        if (PlayerPrefs.GetInt(ManagerParameters.BREAK_LEVEL_RECORD) == 1) {
            curLevel.color = Color.red;
        }
        if (PlayerPrefs.GetInt(ManagerParameters.BREAK_KILL_RECORD) == 1) {
            curKill.color = Color.red;
        }

        // update new record(s)
        if (PlayerPrefs.GetInt(ManagerParameters.BREAK_LEVEL_RECORD) == 1) {
            for (int i = 0; i < ManagerParameters.CURRENT.Length; i++) {
                if (i >= ManagerParameters.CURRENT.Length - 3) {
                    PlayerPrefs.SetFloat(ManagerParameters.LEVEL[i], PlayerPrefs.GetFloat(ManagerParameters.CURRENT[i]));
                } else {
                    PlayerPrefs.SetInt(ManagerParameters.LEVEL[i], PlayerPrefs.GetInt(ManagerParameters.CURRENT[i]));
                }
            }
        }
        if (PlayerPrefs.GetInt(ManagerParameters.BREAK_KILL_RECORD) == 1) {
            for (int i = 0; i < ManagerParameters.CURRENT.Length; i++) {
                if (i >= ManagerParameters.CURRENT.Length - 3) {
                    PlayerPrefs.SetFloat(ManagerParameters.KILL[i], PlayerPrefs.GetFloat(ManagerParameters.CURRENT[i]));
                } else {
                    PlayerPrefs.SetInt(ManagerParameters.KILL[i], PlayerPrefs.GetInt(ManagerParameters.CURRENT[i]));
                }
            }
        }
    }

    void LoadTexts(TextMeshProUGUI kill, TextMeshProUGUI level, TextMeshProUGUI stats, string[] data) {
        kill.text = "" + PlayerPrefs.GetInt(data[0]);
        level.text = "" + PlayerPrefs.GetInt(data[1]);
        stats.text = "";
        for (int i = data.Length - 3; i < data.Length; i++) {
            stats.text += "" + PlayerPrefs.GetFloat(data[i]) + "\n";
        }
    }

    public void MainMenuButton() {
        SceneManager.LoadScene(ManagerParameters.MENU_SCENE);
    }
}
