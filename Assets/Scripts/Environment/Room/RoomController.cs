using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_roomAudioSource;

    public void EnterRoom()
    {
        m_roomAudioSource.enabled = true;
        m_roomAudioSource.Play();
        gameObject.SetActive(true);
    }

    public void ExitRoom()
    {
        m_roomAudioSource.Stop();
        gameObject.SetActive(false);
    }
}