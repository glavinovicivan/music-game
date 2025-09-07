using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestgiverController : MonoBehaviour, IInteractable
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
    private AudioClip m_introClip;
    [SerializeField]
    private AudioClip m_failureClip;
    [SerializeField]
    private AudioClip m_successClip;

    [Header("Song Testing/Rewards")]
    [SerializeField]
    private SongTestManager m_songTestManager;
    [SerializeField]
    private List<QuestReward> m_rewards = new List<QuestReward>();
    [SerializeField]
    private List<GameObject> m_objectsToActivate = new List<GameObject>();
    [SerializeField]
    private List<GameObject> m_objectsToDeactivate = new List<GameObject>();

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

    #region StartQuest
    public void StartInteraction()
    {
        m_dialogueSource.clip = m_introClip;
        m_dialogueSource.Play();
        m_backgroundHumSource.Stop();
        StartCoroutine(StartQuestCoroutine(m_introClip.length));
    }

    private IEnumerator StartQuestCoroutine(float _duration)
    {
        yield return new WaitForSeconds(_duration);
        m_playerController.StartRecordingSong();
    }
    #endregion

    #region EndQuest
    public void TestSong(Song _testSong)
    {
        if (m_playerController != null && m_songTestManager.RunSongTests(_testSong))
        {
            foreach (QuestReward questReward in m_rewards)
            {
                questReward.ApplyReward(m_playerController, this);
            }
            foreach (GameObject objectToActivate in m_objectsToActivate)
            {
                objectToActivate.SetActive(true);
            }
            foreach (GameObject objectToDeactivate in m_objectsToDeactivate)
            {
                objectToDeactivate.SetActive(false);
            }
            m_dialogueSource.clip = m_successClip;
            m_dialogueSource.Play();
            m_playerController.UnsubscribeQuestgiver(this);
            StartCoroutine(QuestSuccessCoroutine(m_successClip.length));
        }
        else
        {
            m_dialogueSource.clip = m_failureClip;
            m_dialogueSource.Play();
            StartCoroutine(QuestFailureCoroutine(m_failureClip.length));
        }
    }

    private IEnumerator QuestSuccessCoroutine(float _duration)
    {
        yield return new WaitForSeconds(_duration);
        m_playerController.EndInteraction();
        Destroy(gameObject);
    }

    private IEnumerator QuestFailureCoroutine(float _duration)
    {
        yield return new WaitForSeconds(_duration);
        m_backgroundHumSource.Play();
        m_playerController.EndInteraction();
    }
    #endregion
}
