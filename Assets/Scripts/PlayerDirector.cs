/*
 * PlayerDirector:
 * Controls the player along the track.
 */

using System;
using UnityEngine;

public class PlayerDirector : MonoBehaviour
{
    public static event Action OnPlayerMoveForwards;

    private void OnDestroy()
    {
        OnPlayerMoveForwards = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Moved forwards.");
            OnPlayerMoveForwards?.Invoke();
        }
    }
}
