using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SongController : MonoBehaviour
{
    [SerializeField]
    private List<InstrumentSO> m_instruments;
    [SerializeField]
    private List<ScaleSO> m_scales;

    private List<AudioSource> m_audioSources = new List<AudioSource>();

    public void PlaySong(Song _song)
    {
        for (int i = 0; i < _song.SongLines.Count; i++)
        {
            if (m_audioSources.Count <= i)
            {
                m_audioSources.Add(gameObject.AddComponent<AudioSource>());
            }
            StartCoroutine(PlaySongLine(_song.SongLines[i], m_audioSources[i]));
        }
    }

    public void StopPlaying()
    {
        StopAllCoroutines();
    }

    private IEnumerator PlaySongLine(SongLine _songLine, AudioSource _audioSource)
    {
        InstrumentSO _instrument = m_instruments.FirstOrDefault(x => x.InstrumentName == _songLine.Instrument);
        foreach (Note _note in _songLine.Notes)
        {
            if (_note.Tone == ETones.Pause)
            {
                _audioSource.Stop();
            }
            else
            {
                _audioSource.clip = _instrument.ToneSoundPairings.FirstOrDefault(x => x.Tone == _note.Tone).AudioClip;
                _audioSource.Play();
            }
            yield return new WaitForSeconds(_note.Duration);
        }
        _audioSource.Stop();
    }
}
