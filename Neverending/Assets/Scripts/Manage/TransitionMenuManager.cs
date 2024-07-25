using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionMenuManager : MonoBehaviour
{
    public TextMeshProUGUI stats;
    void Start() {
        string[] strings = {"Level: ", "Kill Count: ", "Attack: ", "Defense: ", "Max Health: "};
        stats.text = "";
        for (int i = 0; i < ManagerParameters.CURRENT.Length; i++) {    
            if (i >= ManagerParameters.CURRENT.Length - 3) {
                stats.text += strings[i] + PlayerPrefs.GetFloat(ManagerParameters.CURRENT[i]) + "\n";
            } else {
                stats.text += strings[i] + PlayerPrefs.GetInt(ManagerParameters.CURRENT[i]) + "\n";
            }
        }
    }

    public void MainMenuButton() {
        SceneManager.LoadScene(ManagerParameters.MENU_SCENE);
    }

    public void NextLevelButton() {
        SceneManager.LoadScene(ManagerParameters.GAME_SCENE);
    }
}
