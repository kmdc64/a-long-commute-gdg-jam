using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_totalDistanceLabel;
    [SerializeField] TextMeshProUGUI m_totalHappinessItemsCollectedLabel;
    [SerializeField] TextMeshProUGUI m_totalScoreLabel;

    private void OnEnable()
    {
        m_totalDistanceLabel.text = "Distance Collected: " + PlayerStats.HappinessItemsCollected;
        var score = PlayerStats.DistanceTravelled * PlayerStats.HappinessItemsCollected;
        m_totalScoreLabel.text = "Total Score: " + score;
    }

    public void Action_Replay()
    {
        SceneManager.LoadScene("TestTrack");
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    }

    public void Action_ReturnToMainMenu()
    {
        PlayerStats.SaveScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void Action_QuitGame()
    {
        PlayerStats.SaveScore();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
