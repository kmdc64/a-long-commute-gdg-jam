/*
 * SetPieceCollectionSO:
 * A ScriptableObject containing set pieces at different difficulty levels.
 */

using UnityEngine;

[CreateAssetMenu]
public class SetPieceCollectionSO : ScriptableObject
{
    public string SetId => m_setId;
    public SetPiece[] EasySetPieces => m_easySetPieces;
    public SetPiece[] MediumSetPieces => m_mediumSetPieces;
    public SetPiece[] HardSetPieces => m_hardSetPieces;

    [SerializeField] private string m_setId;
    [SerializeField] private SetPiece[] m_easySetPieces;
    [SerializeField] private SetPiece[] m_mediumSetPieces;
    [SerializeField] private SetPiece[] m_hardSetPieces;
}
