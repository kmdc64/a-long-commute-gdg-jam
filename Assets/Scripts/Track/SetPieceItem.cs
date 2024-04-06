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
        Boost,
        Death
    }

    public static event Action<SetPieceItem> OnAnyItemCollected;

    public ItemTypes ItemType => m_itemType;
    public int Value => m_value;

    [SerializeField] private ItemTypes m_itemType;
    [SerializeField] private int m_value;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        OnAnyItemCollected?.Invoke(this);
        if (ItemType != ItemTypes.Death)
        {
            Destroy(gameObject);
        }
    }
}
