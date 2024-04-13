/*
 * SetPieceDirector:
 * Controls the set pieces that populate the track.
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class SetPieceDirector : MonoBehaviour
{
    [SerializeField] private TrackPopulator m_trackPopulator;
    [SerializeField] private SetPieceCollectionSO m_startingSetCollection;
    [Tooltip("The amount of set pieces placed ahead of the player at any one time.")]
    [SerializeField] private int m_setDistance = 6;

    public bool IsTrackDespawning => m_trackPopulator.IsTrackDespawning;

    private Dictionary<SetPiece.PieceDifficulties, List<SetPiece>> m_set = new();
    private SetPieceCollectionSO m_currentSetCollection;
    private int m_currentMaxDifficulty = 0;
    private bool m_lastPieceStaticObstacle;
    private bool m_lastPieceMovingObstacle;

    public void InitialiseTrack()
    {
        PlayerDirector.OnPlayerMoveForwards += Event_OnPlayerMoveForwards;

        RegisterSet(m_startingSetCollection);
        StartTrack();
    }

    public void ClearTrack()
    {
        PlayerDirector.OnPlayerMoveForwards -= Event_OnPlayerMoveForwards;

        m_trackPopulator.DespawnTrack();
    }

    private void StartTrack()
    {
        m_currentMaxDifficulty = 0;
        m_lastPieceStaticObstacle = false;
        m_lastPieceMovingObstacle = false;

        for (var index = 0; index < m_setDistance; ++index)
        {
            PlaceNextSetPiece();
        }
    }

    private void RegisterSet(SetPieceCollectionSO setCollection)
    {
        m_currentSetCollection = setCollection;
        m_set.Clear();

        var difficulties = Enum.GetValues(typeof(SetPiece.PieceDifficulties));
        for (var index = 0; index < difficulties.Length; ++index)
        {
            var pieces = m_currentSetCollection.SetPieces.FindAll(x => ((int)x.DifficultyLevel == index));
            m_set.Add((SetPiece.PieceDifficulties)index, pieces);
        }
    }

    private void PlaceNextSetPiece()
    {
        // Raise the baseline difficulty if the happiness has dropped into the depression zone.
        var maximumDifficultyOfSetCollection = Mathf.Min(m_currentSetCollection.DifficultyConfigs.Length - 1, m_currentMaxDifficulty);
        var proposedBaselineDifficulty = (HappinessTracker.InDepressedState() ? (maximumDifficultyOfSetCollection - 1) : 0);
        var actualBaselineDifficulty = Mathf.Clamp(proposedBaselineDifficulty, 0, maximumDifficultyOfSetCollection);
        var pieceDifficulty = UnityEngine.Random.Range(actualBaselineDifficulty, m_currentMaxDifficulty + 1);
        var pieceList = m_set[(SetPiece.PieceDifficulties)pieceDifficulty];
        var pieceToSpawn = pieceList[UnityEngine.Random.Range(0, pieceList.Count)];
        var isValid = CheckPieceIsValid(pieceToSpawn);
        while (!isValid)
        {
            // Keep selecting at random till we get a valid piece.
            pieceToSpawn = pieceList[UnityEngine.Random.Range(0, pieceList.Count)];
            isValid = CheckPieceIsValid(pieceToSpawn);
        }
        m_trackPopulator.SpawnSetPiece(pieceToSpawn);
        m_lastPieceStaticObstacle = (pieceToSpawn.PieceType is SetPiece.PieceTypes.Slide or SetPiece.PieceTypes.Jump);
        m_lastPieceMovingObstacle = (pieceToSpawn.PieceType is SetPiece.PieceTypes.MovingObstacle);
    }

    private bool CheckPieceIsValid(SetPiece piece)
    {
        var isStaticObstacle = (piece.PieceType is SetPiece.PieceTypes.Slide or SetPiece.PieceTypes.Jump);
        var isMovingObstacle = (piece.PieceType is SetPiece.PieceTypes.MovingObstacle);
        if ((isStaticObstacle || isMovingObstacle) && m_trackPopulator.CurrentSetLength == 0)
            return false; // First track piece should never be an obstacle.

        if (!isStaticObstacle && !isMovingObstacle)
            return true;

        var lastPieceWasObstacle = m_lastPieceStaticObstacle || m_lastPieceMovingObstacle;
        if (!lastPieceWasObstacle)
            return true;

        return false;
    }

    private void RemovePassedSetPiece()
    {
        m_trackPopulator.DespawnPassedSetPiece();
    }

    private void AdjustDifficulty()
    {
        if ((m_currentSetCollection.DifficultyConfigs.Length - 1) == (m_currentMaxDifficulty))
            return; //Max difficulty reached. No progression possible.

        var currentDifficultyConfig = m_currentSetCollection.DifficultyConfigs[m_currentMaxDifficulty];
        if (PlayerStats.DistanceTravelled >= currentDifficultyConfig.DistanceToProgress)
        {
            m_currentMaxDifficulty++;
        }
    }

    private void Event_OnPlayerMoveForwards(int spacesMoved)
    {
        for (var index = 0; index < spacesMoved; ++index)
        {
            PlaceNextSetPiece();
            RemovePassedSetPiece();
        }

        AdjustDifficulty();
    }
}
