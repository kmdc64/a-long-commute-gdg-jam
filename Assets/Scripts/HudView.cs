using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_distanceLabel;

    private void Start()
    {
        PlayerDirector.OnPlayerMoveForwards += Event_OnPlayerMovedForward;
    }

    private void Event_OnPlayerMovedForward(int spacesMoved)
    {
        m_distanceLabel.text = "Distance: " + PlayerStats.DistanceTravelled;
    }

    public void Action_TriggerGameOver()
    {
        SceneManager.LoadScene("Game Over Screen", LoadSceneMode.Additive);
    }
}
