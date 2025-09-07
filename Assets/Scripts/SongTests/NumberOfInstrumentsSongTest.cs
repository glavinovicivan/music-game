using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NumberOfInstrumentsSongTest", menuName = "ScriptableObjects/SongTests/NumberOfInstrumentsSongTest")]
public class NumberOfInstrumentsSongTest : SongTest
{
    public int NumberOfInstruments;
    protected override bool SongCorrectCheck(Song _song)
    {
        List<string> instruments = new List<string>();
        foreach (SongLine songLine in _song.SongLines)
        {
            if (!instruments.Contains(songLine.Instrument))
            {
                instruments.Add(songLine.Instrument);
            }
        }
        return instruments.Count >= NumberOfInstruments;
    }
}
