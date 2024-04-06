/*
 * SetPieceDirector:
 * Controls the set pieces that populate the track.
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class SetPieceDirector : MonoBehaviour
{
    public static event Action OnNewRunStarted;

    [SerializeField] private TrackPopulator m_trackPopulator;
    [SerializeField] private SetPieceCollectionSO m_startingSetCollection;
    [Tooltip("The amount of set pieces placed ahead of the player at any one time.")]
    [SerializeField] private int m_setDistance = 6;

    private Dictionary<SetPiece.PieceDifficulties, List<SetPiece>> m_set = new();
    private SetPieceCollectionSO m_currentSetCollection;
    private int m_currentDifficulty = 0;

    private void Start()
    {
        PlayerDirector.OnPlayerMoveForwards += Event_OnPlayerMoveForwards;

        RegisterSet(m_startingSetCollection);
        StartTrack();
    }

    public void StartTrack()
    {
        for (var index = 0; index < m_setDistance; ++index)
        {
            PlaceNextSetPiece();
        }

        OnNewRunStarted?.Invoke();
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
        var pieceDifficulty = UnityEngine.Random.Range(0, m_currentDifficulty);
        var pieceList = m_set[(SetPiece.PieceDifficulties)pieceDifficulty];
        var pieceToSpawn = pieceList[UnityEngine.Random.Range(0, pieceList.Count)];
        m_trackPopulator.SpawnSetPiece(pieceToSpawn);
    }

    private void RemovePassedSetPiece()
    {
        m_trackPopulator.DespawnPassedSetPiece();
    }

    private void Event_OnPlayerMoveForwards(int spacesMoved)
    {
        for (var index = 0; index < spacesMoved; ++index)
        {
            PlaceNextSetPiece();
            RemovePassedSetPiece();
        }
    }
}
