using UnityEngine;

[CreateAssetMenu(fileName = "DescendingToneTest", menuName = "ScriptableObjects/SongTests/DescendingToneTest")]
public class DescendingToneTest : SongTest
{
    protected override bool SongCorrectCheck(Song _song)
    {
        foreach (SongLine _songLine in _song.SongLines)
        {
            bool isSongDescending = true;
            int currentNoteHeight = (int)ETones.Pause;

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
                bool isNoteDescending = noteHeightIndex <= currentNoteHeight;
                if (isNoteDescending)
                {
                    currentNoteHeight = noteHeightIndex;
                }
                else
                {
                    isSongDescending = false;
                }
            }
            if (isSongDescending)
            {
                return true;
            }
        }
        return false;
    }
}
