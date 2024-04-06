/*
 * SetPieceItem:
 * A collectable item that is spawned on the track.
 */

using System;
using UnityEngine;

public class SetPieceItem : MonoBehaviour
{
    public enum ItemTypes
    {
        HappinessGain,
        HappinessDrain,
        Boost
    }

    public static event Action<SetPieceItem> OnAnyItemCollected;

    public ItemTypes ItemType => m_itemType;

    [SerializeField] private ItemTypes m_itemType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        OnAnyItemCollected?.Invoke(this);
        Destroy(gameObject);
    }
}
