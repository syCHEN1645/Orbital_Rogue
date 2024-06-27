using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{
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
    }

    // press this button to go to main menu
    public void QuitLevelButton() {
        SceneManager.LoadScene(MENU_SCENE);
    }

    public void PauseGame() {
        Time.timeScale = 0;
        // hide pause button, show resume button
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        // hide resume button, show pause button
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
    }
}
