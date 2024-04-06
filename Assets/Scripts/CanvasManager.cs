using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    private GameObject m_playerStats;

    void Start()
    {
        m_playerStats = GameObject.Find("Player");
        m_playerStats.GetComponent ("playerStats");
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
}
