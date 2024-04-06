/*
 * PlayerStats:
 * Tracks player metrics.
 */

using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int DistanceTravelled { get; private set; }
    public static int HappinessItemsCollected { get; private set; }
    public static int DepressantItemsCollected { get; private set; }
    public static int BoostItemsCollected { get; private set; }

    void Start()
    {
        PlayerDirector.OnPlayerMoveForwards += Event_OnPlayerMovesForwards;
        SetPieceItem.OnAnyItemCollected += Event_OnAnyItemCollected;
    }

    private void Event_OnPlayerMovesForwards()
    {
        DistanceTravelled++;
    }

    private void Event_OnAnyItemCollected(SetPieceItem item)
    {
        switch (item.ItemType)
        {
            case SetPieceItem.ItemTypes.HappinessGain:
                HappinessItemsCollected++;
                break;
            case SetPieceItem.ItemTypes.HappinessDrain:
                DepressantItemsCollected++;
                break;
            case SetPieceItem.ItemTypes.Boost:
                BoostItemsCollected++;
                break;
            default:
                Debug.LogError("Item type not supported.");
                break;
        }
    }
}
