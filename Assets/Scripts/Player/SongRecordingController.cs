using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SongRecordingController : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_playbackSource;

    [SerializeField]
    private List<InstrumentSO> m_instruments;
    [SerializeField]
    private List<ScaleSO> m_scales;

    [SerializeField]
    private int m_numberOfTracks = 1;

    private Song m_recordedSong;
    private SongLine m_currentRecordedLine;
    private ETones m_currentRecordedTone;

    private bool m_isRecordingTone;
    private bool m_isRecordingSong;

    private int m_currentInstrumentIndex;

    private DateTime m_timeSinceLastTonePress;
    private bool m_trackPauseDuration;

    #region PhaseControls
    public void StartRecordingSong()
    {
        if (!m_isRecordingSong)
        {
            m_recordedSong = new Song();
            m_currentRecordedLine = new SongLine() { Instrument = m_instruments[m_currentInstrumentIndex].InstrumentName, Notes = new List<Note>() };
            m_isRecordingSong = true;
            m_trackPauseDuration = false;
        }
    }

    public bool AdvanceRecordingPhase()
    {
        if (m_recordedSong.SongLines.Count == m_numberOfTracks - 1)
        {
            m_recordedSong.AddSongLine(m_currentRecordedLine);
            m_isRecordingSong = false;
            return true;
        }

        m_recordedSong.AddSongLine(m_currentRecordedLine);
        m_currentRecordedLine = new SongLine() { Instrument = m_instruments[m_currentInstrumentIndex].InstrumentName, Notes = new List<Note>() };
        m_trackPauseDuration = false;

        return false;
    }

    public void UndoRecording()
    {
        m_recordedSong.UndoSongLine();
    }

    public void StartRecordingTone(int _toneNumber)
    {
        if (m_isRecordingSong && !m_isRecordingTone)
        {
            if (m_trackPauseDuration)
            {
                float pauseDuration = (float)(DateTime.Now - m_timeSinceLastTonePress).TotalSeconds;
                m_currentRecordedLine.Notes.Add(new Note() { Tone = ETones.Pause, Duration = pauseDuration });
                m_trackPauseDuration = false;
            }

            m_currentRecordedTone = m_scales[0].Tones[_toneNumber-1];
            m_playbackSource.clip = m_instruments[m_currentInstrumentIndex].ToneSoundPairings.FirstOrDefault(x => x.Tone == m_currentRecordedTone).AudioClip;
            m_playbackSource.Play();
            m_isRecordingTone = true;
        }
    }

    public void EndRecordingTone(float _duration)
    {
        if (m_isRecordingSong)
        {
            m_currentRecordedLine.Notes.Add(new Note() { Tone = m_currentRecordedTone, Duration = _duration });
            m_isRecordingTone = false;
            m_playbackSource.Stop();
            m_timeSinceLastTonePress = DateTime.Now;
            m_trackPauseDuration = true;
        }
    }
    #endregion

    #region QuestRewards
    public void AddInstrument(InstrumentSO _instrument)
    {
        if (!m_instruments.Contains(_instrument))
        {
            m_instruments.Add(_instrument);
        }
    }

    public void AddScale(ScaleSO _scale)
    {
        if (!m_scales.Contains(_scale))
        {
            m_scales.Add(_scale);
        }
    }

    public void AddSongTracks(int _tracksToAdd)
    {
        m_numberOfTracks += _tracksToAdd;
    }
    #endregion

    public void ChangeInstrument()
    {
        m_currentInstrumentIndex = (m_currentInstrumentIndex + 1) % m_instruments.Count;

        if(m_isRecordingSong && m_currentRecordedLine.Notes.Count == 0)
        {
            m_currentRecordedLine = new SongLine() { Instrument = m_instruments[m_currentInstrumentIndex].name, Notes = new List<Note>() };
        }
    }

    public Song GetRecordedSong()
    {
        return m_recordedSong;
    }
}
