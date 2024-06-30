using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private Button button;
    private bool active;
    public TextMeshProUGUI buttonName;
    // Start is called before the first frame update
    void Start()
    {
        active = true;
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            // when game is active, this button is pause button
            button.onClick.AddListener(PauseGame);
        } else {
            // when game is inactive, this button is resume button
            button.onClick.AddListener(ResumeGame);
        }
    }

    void PauseGame() {
        active = false;
        // change name to "resume" after click
        buttonName.text = "Resume";
        Time.timeScale = 0;
    }

    void ResumeGame() {
        active = true;
        // change name to "pause" after click
        buttonName.text = "Pause";
        Time.timeScale = 1;
    }
}
