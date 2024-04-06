using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    GameObject playerStats;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player");
        playerStats.GetComponent ("playerStats");
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
