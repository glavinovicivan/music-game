using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NumberOfTonesSongTest", menuName = "ScriptableObjects/SongTests/NumberOfTonesSongTest")]
public class NumberOfTonesSongTest : SongTest
{
    public int NumberOfTones;
    protected override bool SongCorrectCheck(Song _song)
    {
        int numberOfTones = 0;
        foreach (SongLine songLine in _song.SongLines)
        {
            List<Note> notes = songLine.Notes.Where(x => x.Tone != ETones.Pause).ToList();
            if (notes.Count > numberOfTones)
            {
                numberOfTones = notes.Count;
            }
        }
        return numberOfTones >= NumberOfTones;
    }
}
