using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecordSceneManager : MonoBehaviour
{
    public TextMeshProUGUI levelRecordKill, levelRecordLevel, levelRecordStats, 
        killRecordKill, killRecordLevel, killRecordStats;
    public Button mainMenuButton;
    
    void Start()
    {
        // load all stats (2 sets: kill-record, level-record)
        LoadTexts(levelRecordKill, levelRecordLevel, levelRecordStats, ManagerParameters.LEVEL);
        LoadTexts(killRecordKill, killRecordLevel, killRecordStats, ManagerParameters.KILL);
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

