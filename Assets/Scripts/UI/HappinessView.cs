/*
 * HappinessView:
 * UI view of the happiness stat.
 */

using UnityEngine;
using UnityEngine.UI;

public class HappinessView : MonoBehaviour
{
    [SerializeField] private Image m_happinessBar;
    [SerializeField] private AnimationCurve m_colourifyCurve;
    [SerializeField] private AnimationCurve m_bleachifyCurve;
    [SerializeField] private Material m_colourMaterial;

    private void Awake()
    {
        HappinessTracker.OnHappinessUpdated += Event_OnHappinessUpdated;
    }

    private void OnDestroy()
    {
        HappinessTracker.OnHappinessUpdated -= Event_OnHappinessUpdated;
    }

    private void Event_OnHappinessUpdated(float normalisedHappinessValue)
    {
        m_happinessBar.fillAmount = normalisedHappinessValue;
        var colourifyValue = 1f - m_colourifyCurve.Evaluate(normalisedHappinessValue);
        m_colourMaterial.SetFloat("_Greyscaleify", colourifyValue);
        var bleachifyValue = m_bleachifyCurve.Evaluate(normalisedHappinessValue);
        m_colourMaterial.SetFloat("_Brighten", bleachifyValue);
    }
}
