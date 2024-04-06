/*
 * PlayerDirector:
 * Controls the player along the track.
 */

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDirector : MonoBehaviour
{
    public static event Action OnPlayerMoveForwards;

    private void OnDestroy()
    {
        OnPlayerMoveForwards = null;
    }

    public void OnForwards(InputAction.CallbackContext context)
    {
        Debug.Log("Moved forwards.");
        if (context.phase == InputActionPhase.Performed)
        {
            OnPlayerMoveForwards?.Invoke();
        }
    }
}
