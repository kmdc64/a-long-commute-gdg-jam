/*
 * SetPieceItem:
 * A collidable item that is spawned on the track.
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
    public event Action OnItemCollided;

    public ItemTypes ItemType => m_itemType;
    public int Value => m_value;

    [SerializeField] protected ItemTypes m_itemType;
    [SerializeField] private int m_value;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        OnItemCollided?.Invoke();
        OnAnyItemCollected?.Invoke(this);
        if (ItemType != ItemTypes.Death)
        {
            Destroy(gameObject);
        }
    }
}
