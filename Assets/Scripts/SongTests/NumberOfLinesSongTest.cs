using UnityEngine;

[CreateAssetMenu(fileName = "NumberOfLinesSongTest", menuName = "ScriptableObjects/SongTests/NumberOfLinesSongTest")]
public class NumberOfLinesSongTest : SongTest
{
    public int NumberOfLines;
    protected override bool SongCorrectCheck(Song _song)
    {
        return _song.SongLines.Count >= NumberOfLines;
    }
}
