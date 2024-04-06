/*
 * SetPiece:
 * A single set piece that can be spawned into the track.
 */

using UnityEngine;

public class SetPiece : MonoBehaviour
{
    public enum PieceTypes
    {
        Flat,
        Choice,
        Jump,
        Slide
    }

    public PieceTypes PieceType => m_pieceType;

    [SerializeField] private PieceTypes m_pieceType;
}
