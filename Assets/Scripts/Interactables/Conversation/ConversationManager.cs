using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour, IInteractable
{
    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource m_backgroundHumSource;
    [SerializeField]
    private AudioSource m_playerCloseEnoughSource;
    [SerializeField]
    private AudioSource m_dialogueSource;

    [Header("World Map Clips")]
    [SerializeField]
    private AudioClip m_backgroundHumAudioClip;
    [SerializeField]
    private AudioClip m_playerCloseEnoughClip;

    [Header("Dialogue Clips")]
    [SerializeField]
    private AudioClip m_conversationClip;

    [Header("Rewards")]
    [SerializeField]
    private List<QuestReward> m_rewards = new List<QuestReward>();

    [Header("Destruction Settings")]
    [SerializeField]
    private bool m_destroyOnEnd;

    private PlayerController m_playerController;

    void Start()
    {
        m_backgroundHumSource.clip = m_backgroundHumAudioClip;
        m_playerCloseEnoughSource.clip = m_playerCloseEnoughClip;

        m_backgroundHumSource.loop = true;
        m_backgroundHumSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            if (m_playerCloseEnoughSource != null)
            {
                m_playerCloseEnoughSource.Play();
            }

            playerController.SubscribeQuestgiver(this);
            m_playerController = playerController;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            playerController.UnsubscribeQuestgiver(this);
            m_playerController = null;
        }
    }

    public void StartInteraction()
    {
        m_dialogueSource.clip = m_conversationClip;
        m_dialogueSource.Play();
        m_backgroundHumSource.Stop();
        StartCoroutine(StartConversationCoroutine(m_conversationClip.length));
    }

    private IEnumerator StartConversationCoroutine(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        foreach (QuestReward questReward in m_rewards)
        {
            questReward.ApplyReward(m_playerController, this);
        }

        m_playerController.UnsubscribeQuestgiver(this);
        m_playerController.EndInteraction();

        if (m_destroyOnEnd)
        {
            Destroy(gameObject);
        }
    }
}
