/*
 * HudView:
 * UI view of the in-game HUD.
 */

using TMPro;
using UnityEngine;

public class HudView : MonoBehaviour
{
    private const string ScoreText = "Score: {0}";

    [SerializeField] TextMeshProUGUI m_scoreLabel;

    private void Start()
    {
        PlayerDirector.OnPlayerMoveForwards += Event_OnPlayerMovedForward;
    }

    private void Event_OnPlayerMovedForward(int spacesMoved)
    {
        m_scoreLabel.text = string.Format(ScoreText, PlayerStats.DistanceTravelled);
    }
}
