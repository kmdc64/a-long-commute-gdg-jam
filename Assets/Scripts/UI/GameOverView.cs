/*
 * GameOverView:
 * UI view of the Game Over screen.
 */

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour
{
    private const string DistanceText = "Distance Travelled: {0}";
    private const string HappinessText = "Muffins Collected: {0}";
    private const string HighScoreText = "High Score: {0}";
    private const string TotalScoreText = "Total Score: {0}";

    [SerializeField] TextMeshProUGUI m_totalDistanceLabel;
    [SerializeField] TextMeshProUGUI m_totalHappinessItemsCollectedLabel;
    [SerializeField] TextMeshProUGUI m_highScoreLabel;
    [SerializeField] TextMeshProUGUI m_totalScoreLabel;
    [SerializeField] TextMeshProUGUI m_titleLabel;
    [SerializeField] string[] m_gameOverTitles;

    private void OnEnable()
    {
        m_titleLabel.text = m_gameOverTitles[UnityEngine.Random.Range(0, m_gameOverTitles.Length - 1)];
    }

    private void Update()
    {
        m_totalDistanceLabel.text = string.Format(DistanceText, PlayerStats.DistanceTravelled);
        m_highScoreLabel.text = string.Format(HighScoreText, PlayerStats.HighScore);
        var score = PlayerStats.CurrentScore;
        m_totalScoreLabel.text = string.Format(TotalScoreText, score);
        m_totalHappinessItemsCollectedLabel.text = string.Format(HappinessText, PlayerStats.HappinessItemsCollected);
    }

    public void Action_Replay()
    {
        SceneManager.UnloadSceneAsync("s_game_over");
        GameFlow.SpawnNewTrack();
        GameFlow.StartRun(saveProgress: true);
    }

    public void Action_ReturnToMainMenu()
    {
        SceneManager.UnloadSceneAsync("s_game_over");
        GameFlow.ReturnToMainMenu(saveProgress: true);
    }
}
