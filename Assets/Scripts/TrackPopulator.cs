/*
 * TrackPopulator:
 * Places and despawns set pieces on the track.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPopulator : MonoBehaviour
{
    public event Action OnTrackDespawned;

    [SerializeField] private Transform m_trackRoot;
    [SerializeField] private AnimationCurve m_despawnScaleCurve;
    [SerializeField] private float m_despawnDuration = 0.75f;
    [SerializeField] private float m_despawnChainDelay = 0.35f;
    [SerializeField] private Vector3 m_setPieceSpacing = new Vector3(0f, 0f, 2f);

    private readonly List<SetPiece> m_setPieces = new();
    private Coroutine m_despawnCoroutine;

    private void OnEnable()
    {
        PlayerDirector.OnPlayerMoveForwards += Event_OnPlayerMoveForwards;
    }

    private void OnDisable()
    {
        PlayerDirector.OnPlayerMoveForwards -= Event_OnPlayerMoveForwards;

        if (m_despawnCoroutine != null)
        {
            StopCoroutine(m_despawnCoroutine);
            m_despawnCoroutine = null;
        }
    }

    public void SpawnSetPiece(SetPiece piece)
    {
        var newSetPiece = Instantiate(piece, m_trackRoot);
        var previousPiecePosition = (m_setPieces.Count > 0) ? m_setPieces[^1].transform.position : m_trackRoot.position;
        newSetPiece.transform.position = previousPiecePosition + m_setPieceSpacing;
        m_setPieces.Add(newSetPiece);
    }

    public void DespawnPassedSetPiece()
    {
        if (m_setPieces.Count == 0)
            return;

        DespawnSetPiece(m_setPieces[0]);
    }

    public void DespawnTrack()
    {
        if (m_despawnCoroutine != null)
            return; // A track despawning is in progress.

        m_despawnCoroutine = StartCoroutine(CompleteTrackDespawning());
    }

    private void DespawnSetPiece(SetPiece piece)
    {
        piece.Despawn(m_despawnScaleCurve, m_despawnDuration);
    }

    private IEnumerator CompleteTrackDespawning()
    {
        for (var index = 0; index < m_setPieces.Count; ++index)
        {
            var setPiece = m_setPieces[0];
            DespawnSetPiece(setPiece);
            m_setPieces.RemoveAt(0);
            if (index < m_setPieces.Count - 1)
            {
                yield return new WaitForSeconds(m_despawnChainDelay);
            }
        }

        yield return new WaitForSeconds(m_despawnDuration);
        OnTrackDespawned?.Invoke();
    }

    private void Event_OnPlayerMoveForwards()
    {
        // Move the set pieces back a space, as we move the world around the player.
        for (var index = 0; index < m_setPieces.Count; ++index)
        {
            var setPiece = m_setPieces[index];
            setPiece.transform.position -= m_setPieceSpacing;
        }
    }
}