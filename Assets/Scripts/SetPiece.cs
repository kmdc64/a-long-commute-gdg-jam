/*
 * SetPiece:
 * A single set piece that can be spawned into the track.
 */

using System;
using System.Collections;
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

    public enum PieceDifficulties
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }

    public event Action OnPieceDespawned;

    public PieceTypes PieceType => m_pieceType;
    public PieceDifficulties DifficultyLevel => m_difficultyLevel;

    [SerializeField] private PieceTypes m_pieceType;
    [SerializeField] private PieceDifficulties m_difficultyLevel;

    private Coroutine m_despawnCoroutine;
    private AnimationCurve m_despawnScaleCurve;
    private float m_despawnDuration;
    private float m_despawnTimeElapsed = 0f;

    private void OnEnable()
    {
        if (m_despawnTimeElapsed > 0f)
        {
            m_despawnCoroutine = StartCoroutine(ShrinkAnimation());
        }
    }

    private void OnDisable()
    {
        if (m_despawnCoroutine != null)
        {
            StopCoroutine(m_despawnCoroutine);
            m_despawnCoroutine = null;
        }
    }

    private void OnDestroy()
    {
        OnPieceDespawned?.Invoke();
    }

    public void Despawn(AnimationCurve despawnScaleCurve, float despawnDuration)
    {
        m_despawnScaleCurve = despawnScaleCurve;
        m_despawnDuration = despawnDuration;
        m_despawnCoroutine = StartCoroutine(ShrinkAnimation());
    }

    private IEnumerator ShrinkAnimation()
    {
        var originalScale = transform.localScale;
        while (m_despawnTimeElapsed < m_despawnDuration)
        {
            var normalisedTime = (m_despawnTimeElapsed / m_despawnDuration) * m_despawnScaleCurve.keys[^1].time;
            var scaleEvaluation = m_despawnScaleCurve.Evaluate(normalisedTime);
            transform.localScale = new Vector3(originalScale.x * scaleEvaluation, originalScale.y * scaleEvaluation, originalScale.z * scaleEvaluation);
            m_despawnTimeElapsed += Time.deltaTime;
            yield return null;
        }

        m_despawnTimeElapsed = 0f;
        Destroy(gameObject);
    }
}
