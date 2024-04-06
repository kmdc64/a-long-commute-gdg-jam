/*
 * PlayerDirector:
 * Controls the player along the track.
 */

using System;
using UnityEngine;

public class PlayerDirector : MonoBehaviour
{
    public static event Action OnPlayerMoveForwards;
    public static event Action OnPlayerDeath;

    private const string StepAnimationId = "Step";

    [SerializeField] private Animator m_playerAnimator;
    [SerializeField] private float m_forwardsCooldown = 0.5f;
    [SerializeField] private float m_forwardsCooldownLeeway = 0.2f;

    private readonly int m_stepAnimationHashId = Animator.StringToHash(StepAnimationId);
    private float m_forwardsCooldownTimer;
    private bool m_stepRequested;

    private void Start()
    {
        SetPieceItem.OnAnyItemCollected += Event_OnItemCollected;
    }

    private void OnDestroy()
    {
        OnPlayerMoveForwards = null;
    }

    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        var stepAllowed = (m_forwardsCooldownTimer <= m_forwardsCooldownLeeway);
        if (Input.GetKeyDown(KeyCode.Space) && stepAllowed)
        {
            m_stepRequested = true;
        }

        if (m_forwardsCooldownTimer > 0f)
        {
            m_forwardsCooldownTimer = Mathf.Clamp(m_forwardsCooldownTimer - Time.deltaTime, 0f, Mathf.Infinity);
        }
        else
        {
            if (m_stepRequested)
            {
                OnPlayerMoveForwards?.Invoke();
                m_playerAnimator.SetTrigger(m_stepAnimationHashId);
                m_forwardsCooldownTimer = m_forwardsCooldown;
                m_stepRequested = false;
            }
        }
    }

    private void Event_OnItemCollected(SetPieceItem item)
    {
        switch (item.ItemType)
        {
            case SetPieceItem.ItemTypes.HappinessGain:
                break;
            case SetPieceItem.ItemTypes.HappinessDrain:
                break;
            case SetPieceItem.ItemTypes.Boost:
                break;
            default:
                Debug.LogError("Item type not supported.");
                break;
        }
    }
}
