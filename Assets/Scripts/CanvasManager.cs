using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] TextMeshProUGUI muffins;

    [SerializeField] TextMeshProUGUI totalDistance;
    [SerializeField] TextMeshProUGUI totalMuffins;
    [SerializeField] TextMeshProUGUI totalScore;

    void Start()
    {
       
    }

    private void Update()
    {
        distance.text = "Distance: " + PlayerStats.DistanceTravelled;
        muffins.text = "Muffins: " + PlayerStats.HappinessItemsCollected;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("TestTrack");
        SceneManager.LoadScene("HUD",LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        PlayerStats.SaveScore();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Menu()
    {
        PlayerStats.SaveScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over Screen", LoadSceneMode.Additive);
        totalDistance.text = "Total Distance Travelled: " + PlayerStats.DistanceTravelled;
        totalMuffins.text = "Total Muffins Collected: " + PlayerStats.HappinessItemsCollected;

        int score;
        score = PlayerStats.DistanceTravelled * PlayerStats.HappinessItemsCollected;

        totalScore.text = "Total Score: " + score;
    }
}
