using System.Collections.Generic;
using System;

[Serializable]
public class Song
{
    public List<SongLine> SongLines;

    public Song()
    {
        SongLines = new List<SongLine>();
    }

    public Song(List<SongLine> _songLines)
    {
        SongLines = _songLines;
    }

    public Song(Song _songToCopy)
    {
        SongLines = new List<SongLine>(_songToCopy.SongLines);
    }

    public void AddSongLine(SongLine _songLine)
    {
        SongLines.Add(_songLine);
    }

    public void UndoSongLine()
    {
        if (SongLines.Count > 0)
        {
            SongLines.RemoveAt(SongLines.Count - 1);
        }
    }

    public float GetSongLength()
    {
        float longestSongLineLength = 0;
        foreach (SongLine songLine in SongLines)
        {
            float songLineLength = 0;
            foreach(Note note in songLine.Notes)
            {
                songLineLength += note.Duration;
            }
            if (songLineLength > longestSongLineLength)
            {
                longestSongLineLength = songLineLength;
            }
        }
        return longestSongLineLength;
    }
}
