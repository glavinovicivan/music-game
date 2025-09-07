using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstrumentSongTest", menuName = "ScriptableObjects/SongTests/InstrumentSongTest")]

public class InstrumentSongTest : SongTest
{
    public string InstrumentName;
    protected override bool SongCorrectCheck(Song _song)
    {
        foreach(SongLine songLine in _song.SongLines)
        {
            if(songLine.Instrument == InstrumentName)
            {
                return true;
            }
        }

        return false;
    }
}
