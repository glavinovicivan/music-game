using UnityEngine;

public class RoomTeleporter : MonoBehaviour
{
    [SerializeField]
    private RoomController m_roomExit;
    [SerializeField]
    private RoomController m_roomEntry;
    [SerializeField]
    private Vector3 m_teleportOffset;

    [Header("Audio")]
    [SerializeField]
    private AudioSource m_audioSource;
    [SerializeField]
    private AudioClip m_audioClip;

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.TryGetComponent(out PlayerController _playerController))
        {

            _playerController.TeleportPlayer(m_roomEntry.transform.position + m_teleportOffset);

            m_audioSource.clip = m_audioClip;
            m_audioSource.Play();
            m_roomEntry.EnterRoom();
            m_roomExit.ExitRoom();
        }
    }
}
