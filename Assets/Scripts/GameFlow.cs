/*
 * GameFlow:
 * Manager of the game flow.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public enum GameStates
    {
        Boot,
        Menu,
        Run,
        GameOver,
        Quitting
    }

    [SerializeField] private SetPieceDirector m_setPieceDirector;
    [SerializeField] private int m_startingHappiness = 60;

    public static GameFlow Instance => s_instance;
    public static GameStates GameState => s_gameState;

    private static GameFlow s_instance;
    private static GameStates s_gameState = GameStates.Boot;

    public void Start()
    {
        if (s_instance != null)
        {
            Debug.LogError("There should not be more than one instance of the GameFlow.cs");
        }
        s_instance = this;

        ReturnToMainMenu();
    }

    public static void StartRun(bool saveProgress = false)
    {
        if (saveProgress)
        {
            PlayerStats.SaveScore();
        }

        SceneManager.LoadScene("s_hud", LoadSceneMode.Additive);
        Instance.StartRunInternal();
    }

    public static void ReturnToMainMenu(bool saveProgress = false)
    {
        if (saveProgress)
        {
            PlayerStats.SaveScore();
        }

        SceneManager.LoadScene("s_main_menu", LoadSceneMode.Additive);
        s_instance.m_setPieceDirector.InitialiseTrack();
        s_gameState = GameStates.Menu;
    }

    public static void QuitGame()
    {
        s_gameState = GameStates.Quitting;
        PlayerStats.SaveScore();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public static void StartGameOver()
    {
        s_gameState = GameStates.GameOver;
        s_instance.StartCoroutine(s_instance.PrepareGameOver());
    }

    public static void SpawnNewTrack()
    {
        s_instance.m_setPieceDirector.InitialiseTrack();
    }

    private void StartRunInternal()
    {
        s_gameState = GameStates.Run;
        m_setPieceDirector.StartRun();
        HappinessTracker.SetHappiness(m_startingHappiness);
    }

    private IEnumerator PrepareGameOver()
    {
        SceneManager.UnloadSceneAsync("s_hud");
        s_instance.m_setPieceDirector.ClearTrack();
        while (s_instance.m_setPieceDirector.IsTrackDespawning)
            yield return null;

        SceneManager.LoadScene("s_game_over", LoadSceneMode.Additive);
    }
}
