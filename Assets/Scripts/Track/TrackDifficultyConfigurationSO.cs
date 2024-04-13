/*
 * TrackDifficultyConfigurationSO:
 * A ScriptableObject containing the ID & distance requirement for a difficulty level.
 */

using UnityEngine;

[CreateAssetMenu(menuName = "Track/Scriptable Objects/Difficulty Config")]
public class TrackDifficultyConfigurationSO : ScriptableObject
{
    public string Identifier => m_identifier;
    public int DistanceToProgress => m_distanceToProgress;

    [SerializeField] private string m_identifier;
    [Tooltip("The distance the player must reach in a run to move onto the next difficulty level.")]
    [SerializeField] private int m_distanceToProgress;
}
