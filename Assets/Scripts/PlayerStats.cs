/*
 * PlayerStats:
 * Tracks player metrics.
 */

using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private const string HighScorePrefId = "HighScore";

    public static int HighScore { get; private set; }
    public static int CurrentScore { get; private set; }
    public static bool HighScoreBeat { get; private set; }
    public static int DistanceTravelled { get; private set; }
    public static int HappinessItemsCollected { get; private set; }
    public static int DepressantItemsCollected { get; private set; }
    public static int BoostItemsCollected { get; private set; }

    void Start()
    {
        SetPieceDirector.OnNewRunStarted += Event_OnNewRunStarted;
        PlayerDirector.OnPlayerMoveForwards += Event_OnPlayerMovesForwards;
        PlayerDirector.OnPlayerDeath += Event_OnPlayerDeath;
        SetPieceItem.OnAnyItemCollected += Event_OnAnyItemCollected;

        HighScore = PlayerPrefs.GetInt(HighScorePrefId, 0);
    }

    public static void SaveScore()
    {
        if (!HighScoreBeat)
            return;

        PlayerPrefs.SetInt(HighScorePrefId, CurrentScore);
    }

    private void UpdateScore(int scoreIncrement)
    {
        CurrentScore += scoreIncrement;
    }

    private void ResetScores()
    {
        HighScoreBeat = false;
        CurrentScore = 0;
        DistanceTravelled = 0;
        HappinessItemsCollected = 0;
        DepressantItemsCollected = 0;
        BoostItemsCollected = 0;
    }

    private void Event_OnNewRunStarted()
    {
        ResetScores();
    }

    private void Event_OnPlayerMovesForwards(int spacesMoved)
    {
        DistanceTravelled += spacesMoved;
        UpdateScore(100 * spacesMoved);
    }

    private void Event_OnPlayerDeath()
    {
        if (CurrentScore > HighScore)
        {
            HighScoreBeat = true;
        }
    }

    private void Event_OnAnyItemCollected(SetPieceItem item)
    {
        switch (item.ItemType)
        {
            case SetPieceItem.ItemTypes.HappinessGain:
                HappinessItemsCollected++;
                UpdateScore(15);
                break;
            case SetPieceItem.ItemTypes.HappinessDrain:
                DepressantItemsCollected++;
                break;
            case SetPieceItem.ItemTypes.Boost:
                BoostItemsCollected++;
                UpdateScore(10);
                break;
            default:
                Debug.LogError("Item type not supported.");
                break;
        }
    }
}
