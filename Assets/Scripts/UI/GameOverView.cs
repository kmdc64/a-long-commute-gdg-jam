/*
 * GameOverView:
 * UI view of the Game Over screen.
 */

using TMPro;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    private const string DistanceText = "Distance Collected: {0}";
    private const string ScoreText = "Total Score: {0}";

    [SerializeField] TextMeshProUGUI m_totalDistanceLabel;
    [SerializeField] TextMeshProUGUI m_totalHappinessItemsCollectedLabel;
    [SerializeField] TextMeshProUGUI m_totalScoreLabel;

    private void OnEnable()
    {
        m_totalDistanceLabel.text = string.Format(DistanceText, PlayerStats.HappinessItemsCollected);
        var score = PlayerStats.DistanceTravelled * PlayerStats.HappinessItemsCollected;
        m_totalScoreLabel.text = string.Format(ScoreText, score);
    }

    public void Action_Replay()
    {
        GameFlow.StartRun(saveProgress: true);
    }

    public void Action_ReturnToMainMenu()
    {
        GameFlow.ReturnToMainMenu(saveProgress: true);
    }
}
