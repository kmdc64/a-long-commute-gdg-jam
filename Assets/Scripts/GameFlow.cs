/*
 * GameFlow:
 * Manager of the game flow.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public static void StartRun(bool saveProgress = false)
    {
        if (saveProgress)
        {
            PlayerStats.SaveScore();
        }

        SceneManager.LoadScene("TestTrack");
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    }

    public static void ReturnToMainMenu(bool saveProgress = false)
    {
        if (saveProgress)
        {
            PlayerStats.SaveScore();
        }

        SceneManager.LoadScene("MainMenu");
    }

    public static void QuitGame()
    {
        PlayerStats.SaveScore();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public static void StartGameOver()
    {
        SceneManager.LoadScene("Game Over Screen", LoadSceneMode.Additive);
    }
}
