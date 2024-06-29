using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private const string START_GAME = "PG Test Scene";
    public Canvas settings;
    // Start is called before the first frame update
    void Start() 
    {
        // make sure showing main menu and not showing settings page
        gameObject.SetActive(true);
        settings.gameObject.SetActive(false);
        // initialise saved settings
        settings.GetComponent<VolumeSettings>().Initialise();
    }

    // public void LoadScene(string sceneName) {
    //     SceneManager.LoadScene(sceneName);
    // }

    public void NewGameButton() {
        SceneManager.LoadScene(START_GAME);
    }

    public void QuitGameButton() {
        Application.Quit();
    }

    // press this button to go to settings page
    public void SettingsButton() {
        // main menu disappears
        gameObject.SetActive(false);
        // settings page appears
        settings.gameObject.SetActive(true);
    }

    // press this button to go back from settings page to main menu
    public void BackButton() {
        // settings page disappears
        settings.gameObject.SetActive(false);
        // main menu appears
        gameObject.SetActive(true);
    }
}
