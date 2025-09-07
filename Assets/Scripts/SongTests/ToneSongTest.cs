using UnityEngine;

[CreateAssetMenu(fileName = "ToneSongTest", menuName = "ScriptableObjects/SongTests/ToneSongTest")]
public class ToneSongTest : SongTest
{
    public ETones Tone;
    protected override bool SongCorrectCheck(Song _song)
    {
        foreach (SongLine _songLine in _song.SongLines)
        {
            foreach (Note _note in _songLine.Notes)
            {
                if (_note.Tone == Tone)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
