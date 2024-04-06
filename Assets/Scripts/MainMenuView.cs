
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuView : MonoBehaviour
{
    public void Action_PlayGame()
    {
        SceneManager.LoadScene("TestTrack");
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    }

    public void Action_QuitGame()
    {
        PlayerStats.SaveScore();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Action_OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
