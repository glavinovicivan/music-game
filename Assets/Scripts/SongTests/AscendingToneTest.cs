using UnityEngine;

[CreateAssetMenu(fileName = "AscendingToneTest", menuName = "ScriptableObjects/SongTests/AscendingToneTest")]
public class AscendingToneTest : SongTest
{
    protected override bool SongCorrectCheck(Song _song)
    {
        foreach (SongLine _songLine in _song.SongLines)
        {
            bool isSongAscending = true;
            int currentNoteHeight = 0;

            if (_songLine.Notes.Count < 2)
            {
                continue;
            }

            foreach (Note _note in _songLine.Notes)
            {
                if (_note.Tone == ETones.Pause)
                {
                    continue;
                }

                int noteHeightIndex = (int)(_note.Tone);
                bool isNoteAscending = noteHeightIndex >= currentNoteHeight;
                if (isNoteAscending)
                {
                    currentNoteHeight = noteHeightIndex;
                }
                else
                {
                    isSongAscending = false;
                }
            }

            if (isSongAscending)
            {
                return true;
            }
        }
        return false;
    }
}
