/*
 * SetPieceMovingItem:
 * A collidable item that is spawned on the track and moves from A-B.
 */

using UnityEngine;

public class SetPieceMovingItem : SetPieceItem
{
    [SerializeField] private SetPieceItem m_itemToSpawn;
    [SerializeField] private Transform m_pointA;
    [SerializeField] private Transform m_pointB;
    [SerializeField] private float m_spawnInterval = 3f;
    [SerializeField] private float m_movementDuration = 1f;

    private bool m_allowMoving;
    private SetPieceItem m_currentItem;
    private float m_spawnTimer;
    private float m_moveTimer;

    private void FixedUpdate()
    {
        if (GameFlow.GameState != GameFlow.GameStates.Run)
            return;

        if (m_currentItem == null)
        {
            if (m_spawnTimer >= m_spawnInterval)
            {
                SpawnNewItem();
                m_allowMoving = true;
            }
            else
            {
                m_spawnTimer += Time.fixedDeltaTime;
            }
        }
        else if (m_allowMoving)
        {
            var lerpValue = m_moveTimer / m_movementDuration;
            m_currentItem.transform.position = Vector3.Lerp(m_pointA.position, m_pointB.position, lerpValue);
            m_moveTimer += Time.fixedDeltaTime;
            if (m_moveTimer > m_movementDuration)
            {
                m_moveTimer = 0f;
                Destroy(m_currentItem.gameObject);
                m_currentItem = null;
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        return;
    }

    private void SpawnNewItem()
    {
        var newItem = Instantiate(m_itemToSpawn);
        newItem.transform.position = m_pointA.position;
        newItem.transform.SetParent(transform, worldPositionStays: true);
        m_itemToSpawn.OnItemCollided += () =>
        {
            m_allowMoving = false;
        };
        m_currentItem = newItem;
        m_spawnTimer = 0f;
    }
}
