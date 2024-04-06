/*
 * MainMenuView:
 * UI view of the Main Menu screen.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuView : MonoBehaviour
{
    public void Action_PlayGame()
    {
        GameFlow.StartRun();
    }

    public void Action_QuitGame()
    {
        GameFlow.QuitGame();
    }

    public void Action_OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
