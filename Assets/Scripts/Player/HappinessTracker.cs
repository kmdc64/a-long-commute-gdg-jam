/*
 * HappinessTracker:
 * Tracks player happiness levels.
 */

using System;
using UnityEngine;

public class HappinessTracker
{
    private const float DepressionThreshold = 0.3f;

    public static event Action<float> OnHappinessUpdated; // <float> - Normalised Happiness.

    private static int s_maximumHappiness = 100;
    private static int s_currentHappiness = 0;

    public static void SetHappiness(int happiness)
    {
        s_currentHappiness = Mathf.Clamp(happiness, 0, s_maximumHappiness);
        OnHappinessUpdated?.Invoke(GetNormalisedHappiness());
    }

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

    public static bool InDepressedState()
    {
        return (GetNormalisedHappiness() <= DepressionThreshold);
    }

    private static float GetNormalisedHappiness()
    {
        return (float)s_currentHappiness / (float)s_maximumHappiness;
    }
}
