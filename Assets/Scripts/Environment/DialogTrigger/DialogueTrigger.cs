using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_dialogueAudioSource;
    [SerializeField]
    private AudioClip m_dialogueAudioClip;
    [SerializeField]
    private bool m_destroyOnEnd;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.TryGetComponent(out PlayerController playerController))
        {
            m_dialogueAudioSource.clip = m_dialogueAudioClip;
            m_dialogueAudioSource.Play();
            playerController.DisablePlayerMovement();
            StartCoroutine(EndDialogue(m_dialogueAudioClip.length, playerController));
        }
    }

    private IEnumerator EndDialogue(float _duration, PlayerController _playerController)
    {
        yield return new WaitForSeconds(_duration);
        _playerController.EnablePlayerMovement();
        if (m_destroyOnEnd)
        {
            Destroy(gameObject);
        }
    }
}
