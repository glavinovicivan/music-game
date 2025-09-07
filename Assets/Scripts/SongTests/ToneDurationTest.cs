using UnityEngine;

[CreateAssetMenu(fileName = "ToneDurationTest", menuName = "ScriptableObjects/SongTests/ToneDurationTest")]
public class ToneDurationTest : SongTest
{
    public float NoteDuration;

    protected override bool SongCorrectCheck(Song _song)
    {
        foreach (SongLine _songLine in _song.SongLines)
        {
            foreach (Note _note in _songLine.Notes)
            {
                if(_note.Tone == ETones.Pause)
                {
                    continue;
                }
                if (_note.Duration >= NoteDuration)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
