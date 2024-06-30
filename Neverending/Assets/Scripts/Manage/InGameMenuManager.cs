using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{
<<<<<<< Updated upstream
    private const string MENU_SCENE = "Menu Scene";
    private GameObject pauseButton;
    private GameObject resumeButton;
    void Start()
    {
        pauseButton = GameObject.Find("Pause Button");
        resumeButton = GameObject.Find("Resume Button");
        // make sure showing pause button and hiding resume button
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
=======
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
>>>>>>> Stashed changes
    }

    // press this button to go to main menu
    public void QuitLevelButton() {
<<<<<<< Updated upstream
        SceneManager.LoadScene(MENU_SCENE);
=======
        SceneManager.LoadScene(ManagerParameters.MENU_SCENE);
>>>>>>> Stashed changes
    }

    public void PauseGame() {
        Time.timeScale = 0;
<<<<<<< Updated upstream
        // hide pause button, show resume button
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
=======
        // hide pause button, show resume button and settings button
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
>>>>>>> Stashed changes
    }

    public void ResumeGame() {
        Time.timeScale = 1;
<<<<<<< Updated upstream
        // hide resume button, show pause button
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
=======
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
>>>>>>> Stashed changes
    }
}
