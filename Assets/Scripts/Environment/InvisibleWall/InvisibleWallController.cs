using System.Collections;
using UnityEngine;

public class InvisibleWallController : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_staticAudioSource;
    [SerializeField]
    private AudioClip m_collisionAudioClip;
    [SerializeField]
    private AudioClip m_loopingAudioClip;
    [SerializeField]


    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.TryGetComponent(out PlayerController _playerController))
        {
            Debug.Log("Starting Wall Collision");

            m_staticAudioSource.clip = m_collisionAudioClip;
            m_staticAudioSource.loop = false;
            m_staticAudioSource.Play();
            StartCoroutine(PlayLoopingPartWithDelay(m_collisionAudioClip.length));
        }
    }

    private void OnCollisionStay(Collision _collision)
    {
        if (_collision.gameObject.TryGetComponent(out PlayerController _playerController))
        {
            Debug.Log("Wall Collision Active");
            m_staticAudioSource.transform.position = _collision.GetContact(0).point;
        }
    }

    private void OnCollisionExit(Collision _collision)
    {
        if (_collision.gameObject.TryGetComponent(out PlayerController _playerController))
        {
            m_staticAudioSource.Stop();
        }
    }

    private IEnumerator PlayLoopingPartWithDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        m_staticAudioSource.loop = true;
        m_staticAudioSource.clip = m_loopingAudioClip;
        m_staticAudioSource.Play();
    }
}
