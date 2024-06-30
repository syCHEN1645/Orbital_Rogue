using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionMenuManager : MonoBehaviour
{
    public void MainMenuButton() {
        SceneManager.LoadScene(ManagerParameters.MENU_SCENE);
    }

    public void NextLevelButton() {
        SceneManager.LoadScene(ManagerParameters.GAME_SCENE);
    }
}
