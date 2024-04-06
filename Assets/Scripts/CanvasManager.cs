using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI Distance;
    public TextMeshProUGUI Muffins;

    public TextMeshProUGUI TotalDistance;
    public TextMeshProUGUI TotalMuffins;
    public TextMeshProUGUI FinalScore;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Distance.text = ("Distance: " + PlayerStats.DistanceTravelled);
        Muffins.text = ("Muffins: " + PlayerStats.HappinessItemsCollected);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("TestTrack");
        SceneManager.LoadScene("HUD",LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over Screen", LoadSceneMode.Additive);
        TotalDistance.text = ("Total distance travelled: " + PlayerStats.DistanceTravelled);
        TotalMuffins.text = ("Total muffins collected: " + PlayerStats.HappinessItemsCollected);

        int score;
        score = PlayerStats.DistanceTravelled * PlayerStats.HappinessItemsCollected;

        FinalScore.text = ("Total Score: " + score);
    }
}
