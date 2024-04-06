/*
 * HappinessTracker:
 * Tracks player happiness levels.
 */

using System;
using UnityEngine;

public class HappinessTracker
{
    public static event Action<float> OnHappinessUpdated; // Passes normalised happiness value.

    private static int s_maximumHappiness = 100;
    private static int s_currentHappiness = 0;

    public static void AddHappiness(int happinessIncrement)
    {
        s_currentHappiness = Mathf.Clamp(s_currentHappiness + happinessIncrement, 0, s_maximumHappiness);
        OnHappinessUpdated?.Invoke(GetNormalisedHappiness());
    }

    public static void RemoveHappiness(int happinessDecrement)
    {
        s_currentHappiness = Mathf.Clamp(s_currentHappiness - happinessDecrement, 0, s_maximumHappiness);
        OnHappinessUpdated?.Invoke(GetNormalisedHappiness());
    }

    private static float GetNormalisedHappiness()
    {
        return (float)s_currentHappiness / (float)s_maximumHappiness;
    }
}
