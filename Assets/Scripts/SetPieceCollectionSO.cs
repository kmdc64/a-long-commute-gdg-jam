/*
 * SetPieceCollectionSO:
 * A ScriptableObject containing set pieces at different difficulty levels.
 */

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SetPieceCollectionSO : ScriptableObject
{
    public string SetId => m_setId;
    public List<SetPiece> SetPieces => m_setPieces;

    [SerializeField] private string m_setId;
    [SerializeField] private List<SetPiece> m_setPieces;
}
