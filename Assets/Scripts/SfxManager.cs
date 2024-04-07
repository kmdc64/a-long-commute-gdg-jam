/*
 * SfxManager:
 * Manager of the game's SFX.
 */

using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_stepSfx;
    [SerializeField] private AudioClip m_slideSfx;
    [SerializeField] private AudioClip m_pickupSfx;
    [SerializeField] private AudioClip m_deathSfx;

    private static SfxManager Instance => s_instance;
    private static SfxManager s_instance;

    private void Awake()
    {
        s_instance = this;

        PlayerDirector.OnPlayerDeath += PlayDeathSfx;
        PlayerDirector.OnPlayerMoveForwards += PlayMoveSfx;
        SetPieceItem.OnAnyItemCollected += PlayPickupSfx;
    }

    public static void PlayMoveSfx(int moves)
    {
        switch (moves)
        {
            case 1:
                s_instance.m_audioSource.PlayOneShot(s_instance.m_stepSfx);
                break;
            case 2:
                s_instance.m_audioSource.PlayOneShot(s_instance.m_slideSfx);
                break;
            default:
                break;
        }
    }

    public static void PlayPickupSfx(SetPieceItem item)
    {
        if (item.ItemType == SetPieceItem.ItemTypes.Death)
            return;

        s_instance.m_audioSource.PlayOneShot(s_instance.m_pickupSfx);
    }

    public static void PlayDeathSfx()
    {
        s_instance.m_audioSource.PlayOneShot(s_instance.m_deathSfx);
    }
}
