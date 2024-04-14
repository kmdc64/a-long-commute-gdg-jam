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
    [SerializeField] private Color m_happinessColor;
    [SerializeField] private Color m_neutralColor;
    [SerializeField] private Color m_depressionColor;
    [SerializeField] private AnimationCurve m_happinessToNeutral;
    [SerializeField] private AnimationCurve m_neutralToDepression;

    private void Awake()
    {
        HappinessTracker.OnHappinessUpdated += Event_OnHappinessUpdated;
    }

    private void OnDestroy()
    {
        HappinessTracker.OnHappinessUpdated -= Event_OnHappinessUpdated;
    }
    
    private void UpdateHappinessBar(float normalisedHappinessValue)
    {
        var inDepressedState = HappinessTracker.InDepressedState();
        var curve = inDepressedState ? m_neutralToDepression : m_happinessToNeutral;
        var evaluation = curve.Evaluate(normalisedHappinessValue);
        var colorA = inDepressedState ? m_depressionColor : m_neutralColor;
        var colorB = inDepressedState ? m_neutralColor : m_happinessColor;
        var color = Color.Lerp(colorA, colorB, evaluation);
        m_happinessBar.color = color;
    }

    private void UpdateWorldSaturation(float normalisedHappinessValue)
    {
        m_happinessBar.fillAmount = normalisedHappinessValue;
        var colourifyValue = 1f - m_colourifyCurve.Evaluate(normalisedHappinessValue);
        m_colourMaterial.SetFloat("_Greyscaleify", colourifyValue);
        var bleachifyValue = m_bleachifyCurve.Evaluate(normalisedHappinessValue);
        m_colourMaterial.SetFloat("_Brighten", bleachifyValue);
    }

    private void Event_OnHappinessUpdated(float normalisedHappinessValue)
    {
        UpdateHappinessBar(normalisedHappinessValue);
        UpdateWorldSaturation(normalisedHappinessValue);
    }
}
