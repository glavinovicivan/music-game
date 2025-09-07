using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongControllerSample : MonoBehaviour
{
    [SerializeField]
    private SongController m_songController;
    [SerializeField]
    private SongSO m_song;

    [ContextMenu("Play Song")]
    public void StartSong()
    {
        m_songController.PlaySong(m_song.Song);
    }

    public void StopSong()
    {
        m_songController.StopPlaying();
    }
}
