using UnityEngine;

public abstract class SongTest : ScriptableObject
{
    public bool IsInverse = false;

    protected abstract bool SongCorrectCheck(Song _song);

    public bool IsSongCorrect(Song _song)
    {
        bool songCorrectCheckResult = SongCorrectCheck(_song);
        return IsInverse ? !songCorrectCheckResult : songCorrectCheckResult;
    }
}
