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
    public bool AllowItemSpawns => m_allowItemSpawns;

    [SerializeField] private PieceTypes m_pieceType;
    [SerializeField] private PieceDifficulties m_difficultyLevel;
    [SerializeField] private bool m_allowItemSpawns;
    [Range(0f, 1f)]
    [SerializeField] private float m_chanceOfItemSpawn;
    [SerializeField] private SetPieceItem[] m_spawnableItems;
    [SerializeField] private Transform m_itemSpawnPosition;

    private Coroutine m_despawnCoroutine;
    private AnimationCurve m_despawnScaleCurve;
    private SetPieceItem m_spawnedItem;
    private float m_despawnDuration;
    private float m_despawnTimeElapsed = 0f;

    private void Start()
    {
        if (m_allowItemSpawns)
        {
            AttemptItemSpawn();
        }
    }

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

    private void AttemptItemSpawn()
    {
        var spawnSuccessfull = UnityEngine.Random.Range(0f, 1f) <= m_chanceOfItemSpawn;
        if (spawnSuccessfull)
        {
            var randomItemIndex = UnityEngine.Random.Range(0, m_spawnableItems.Length);
            var randomItem = m_spawnableItems[randomItemIndex];
            m_spawnedItem = Instantiate(randomItem, m_itemSpawnPosition);
            m_spawnedItem.transform.position = m_itemSpawnPosition.position;
            m_spawnedItem.transform.localScale = Vector3.one;
            SetPieceItem.OnAnyItemCollected += DespawnItem;
        }
    }

    private void DespawnItem(SetPieceItem item)
    {
        if (item != m_spawnedItem)
            return;

        m_spawnedItem = null;
    }

    private IEnumerator ShrinkAnimation()
    {
        var originalScale = transform.localScale;
        var originalItemScale = (m_spawnedItem != null) ? m_spawnedItem.transform.localScale : Vector3.zero;

        while (m_despawnTimeElapsed < m_despawnDuration)
        {
            var normalisedTime = (m_despawnTimeElapsed / m_despawnDuration) * m_despawnScaleCurve.keys[^1].time;
            var scaleEvaluation = m_despawnScaleCurve.Evaluate(normalisedTime);
            transform.localScale = new Vector3(originalScale.x * scaleEvaluation, originalScale.y * scaleEvaluation, originalScale.z * scaleEvaluation);
            if (originalItemScale != Vector3.zero)
            {
                m_spawnedItem.transform.localScale = new Vector3(originalItemScale.x * scaleEvaluation, originalItemScale.y * scaleEvaluation, originalItemScale.z * scaleEvaluation);
            }

            m_despawnTimeElapsed += Time.deltaTime;
            yield return null;
        }

        m_despawnTimeElapsed = 0f;
        Destroy(gameObject);
    }
}
