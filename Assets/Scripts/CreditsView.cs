using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsView : MonoBehaviour
{
    public void Action_ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
