using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Canvas settings;
    public Canvas menu;
    public GameObject stats;
    // Start is called before the first frame update
    void Start() 
    {
        // make sure showing main menu and not showing settings page or record page
        menu.gameObject.SetActive(true);
        settings.gameObject.SetActive(false);
        stats.SetActive(false);
        // initialise saved settings
        settings.GetComponent<VolumeSettings>().Initialise();
    }

    // public void LoadScene(string sceneName) {
    //     SceneManager.LoadScene(sceneName);
    // }

    public void NewGameButton() {
        // clear old level, start new game from level 1, set all current data to 0
        for (int i = 0; i < ManagerParameters.CURRENT.Length; i++) {
            if (i >= ManagerParameters.CURRENT.Length - 3) {
                PlayerPrefs.SetFloat(ManagerParameters.CURRENT[i], 0);
            } else {
                PlayerPrefs.SetInt(ManagerParameters.CURRENT[i], 0);
            }
        }
        SceneManager.LoadScene(ManagerParameters.GAME_SCENE);
    }

    public void QuitGameButton() {
        Application.Quit();
    }

    // press this button to go to settings page
    public void SettingsButton() {
        // main menu disappears
        menu.gameObject.SetActive(false);
        // settings page appears
        settings.gameObject.SetActive(true);
    }

    // press this button to go back from settings page to main menu
    public void BackButton() {
        // settings page/record page disappears
        settings.gameObject.SetActive(false);
        stats.SetActive(false);
        // main menu appears
        menu.gameObject.SetActive(true);
    }

    // public void TestFeatureButton() {
    //     SceneManager.LoadScene("Game");
    // }

    public void RecordButton() {
        menu.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        stats.SetActive(true);
    }
}
