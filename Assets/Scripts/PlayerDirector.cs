/*
 * PlayerDirector:
 * Controls the player along the track.
 */

using System;
using UnityEngine;

public class PlayerDirector : MonoBehaviour
{
    public enum ForwardMovements
    {
        Step,
        Slide
    }

    public static event Action<int> OnPlayerMoveForwards; // int: spaces moved
    public static event Action OnPlayerDeath;

    private const string StepAnimationId = "Step";
    private const string SlideAnimationId = "Slide";

    [SerializeField] private Animator m_playerAnimator;
    [SerializeField] private float m_forwardsCooldown = 0.5f;
    [SerializeField] private float m_forwardsCooldownLeeway = 0.2f;
    [SerializeField] private KeyCode m_stepKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode m_slideKey = KeyCode.DownArrow;

    private readonly int m_stepAnimationHashId = Animator.StringToHash(StepAnimationId);
    private readonly int m_slideAnimationHashId = Animator.StringToHash(SlideAnimationId);
    private float m_forwardsCooldownTimer;
    private bool m_stepRequested;
    private bool m_slideRequested;

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
        var forwardAllowed = (m_forwardsCooldownTimer <= m_forwardsCooldownLeeway);
        if (forwardAllowed)
        {
            if (Input.GetKeyDown(m_stepKey))
            {
                m_stepRequested = true;
            }
            else if (Input.GetKeyDown(m_slideKey))
            {
                m_slideRequested = true;
            }
        }

        if (m_forwardsCooldownTimer > 0f)
        {
            m_forwardsCooldownTimer = Mathf.Clamp(m_forwardsCooldownTimer - Time.deltaTime, 0f, Mathf.Infinity);
        }
        else
        {
            ProcessForwardsRequests();
        }
    }
    
    private void ProcessForwardsRequests()
    {
        if (m_stepRequested)
        {
            OnPlayerMoveForwards?.Invoke(1);
            m_playerAnimator.SetTrigger(m_stepAnimationHashId);
            m_forwardsCooldownTimer = m_forwardsCooldown;
            m_stepRequested = false;
        }

        if (m_slideRequested)
        {
            OnPlayerMoveForwards?.Invoke(2);
            m_playerAnimator.SetTrigger(m_slideAnimationHashId);
            m_forwardsCooldownTimer = m_forwardsCooldown;
            m_slideRequested = false;
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
