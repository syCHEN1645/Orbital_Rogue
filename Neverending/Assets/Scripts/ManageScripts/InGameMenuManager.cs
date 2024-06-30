using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private GameObject settingsWindow;
    void Start()
    {
        // make sure showing pause button and hiding resume button
        pauseButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        // settings button is shown only if the game is paused
        settingsButton.gameObject.SetActive(false);
        settingsWindow.gameObject.SetActive(false);
    }

    // press this button to go to main menu
    public void QuitLevelButton() {
        SceneManager.LoadScene(ManagerParameters.MENU_SCENE);
    }

    public void PauseGame() {
        Time.timeScale = 0;
        // hide pause button, show resume button and settings button
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        // hide resume button and settings button, show pause button
        resumeButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    public void Settings() {
        // show settings window
        settingsWindow.SetActive(true);
        // when settings window is open, disable all UIs outside the window
        pauseButton.enabled = false;
        resumeButton.enabled = false;
        quitButton.enabled = false;
        settingsButton.enabled = false;
    }

    public void BackToGame() {
        // hide settings window
        settingsWindow.SetActive(false);
        // enable UIs outside the window
        pauseButton.enabled = true;
        resumeButton.enabled = true;
        quitButton.enabled = true;
        settingsButton.enabled = true;
    }
}
