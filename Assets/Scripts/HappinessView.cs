/*
 * HappinessView:
 * UI view of the happiness stat.
 */

using UnityEngine;
using UnityEngine.UI;

public class HappinessView : MonoBehaviour
{
    [SerializeField] private Image m_happinessBar;

    private void Start()
    {
        HappinessTracker.OnHappinessUpdated += Event_OnHappinessUpdated;
    }

    private void Event_OnHappinessUpdated(float normalisedHappinessValue)
    {
        m_happinessBar.fillAmount = normalisedHappinessValue;
    }
}
