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
    [SerializeField] private int m_startingHappiness = 30;
    [SerializeField] private int m_happinessLossRate = 5;
    [SerializeField] private float m_happinessLossInterval = 0.25f;
    [SerializeField] private float m_forwardsCooldown = 0.5f;
    [SerializeField] private float m_forwardsCooldownLeeway = 0.2f;
    [SerializeField] private KeyCode m_stepKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode m_slideKey = KeyCode.DownArrow;

    private readonly int m_stepAnimationHashId = Animator.StringToHash(StepAnimationId);
    private readonly int m_slideAnimationHashId = Animator.StringToHash(SlideAnimationId);
    private float m_forwardsCooldownTimer;
    private bool m_forwardRequested;
    private bool m_stepRequested;
    private bool m_slideRequested;
    private float m_happinessLossTimer;

    private void Start()
    {
        SetPieceItem.OnAnyItemCollected += Event_OnAnyItemCollected;

        HappinessTracker.AddHappiness(m_startingHappiness);
    }

    private void OnDestroy()
    {
        OnPlayerMoveForwards = null;
    }

    private void Update()
    {
        UpdateHappiness();
        UpdateInput();
    }

    private void UpdateHappiness()
    {
        if (m_happinessLossTimer >= m_happinessLossInterval)
        {
            HappinessTracker.RemoveHappiness(m_happinessLossRate);
            m_happinessLossTimer = 0f;
        }
        else
        {
            m_happinessLossTimer += Time.deltaTime;
        }
    }

    private void UpdateInput()
    {
        var forwardAllowed = (m_forwardsCooldownTimer <= m_forwardsCooldownLeeway) && !m_forwardRequested;
        if (forwardAllowed)
        {
            if (Input.GetKeyDown(m_stepKey))
            {
                m_stepRequested = true;
                m_forwardRequested = true;
            }
            else if (Input.GetKeyDown(m_slideKey))
            {
                m_slideRequested = true;
                m_forwardRequested = true;
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
            m_forwardRequested = false;
        }

        if (m_slideRequested)
        {
            OnPlayerMoveForwards?.Invoke(2);
            m_playerAnimator.SetTrigger(m_slideAnimationHashId);
            m_forwardsCooldownTimer = m_forwardsCooldown * 2f;
            m_slideRequested = false;
            m_forwardRequested = false;
        }
    }

    private void Event_OnAnyItemCollected(SetPieceItem item)
    {
        switch (item.ItemType)
        {
            case SetPieceItem.ItemTypes.HappinessGain:
                HappinessTracker.AddHappiness(item.Value);
                m_happinessLossTimer = 0f;
                break;

            case SetPieceItem.ItemTypes.HappinessDrain:
                HappinessTracker.RemoveHappiness(item.Value);
                break;

            case SetPieceItem.ItemTypes.Boost:
                break;

            case SetPieceItem.ItemTypes.Death:
                OnPlayerDeath?.Invoke();
                break;

            default:
                Debug.LogError("Item type not supported.");
                break;
        }
    }
}
