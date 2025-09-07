using System.Collections.Generic;
using UnityEngine;

public class SongTestManager : MonoBehaviour
{
    [SerializeField]
    private List<SongTest> m_songTests;
    public bool RunSongTests(Song _song)
    {
        foreach (SongTest songTest in m_songTests)
        {
            //Debug.LogError(songTest.name + ":" + songTest.IsSongCorrect(_song));
            if (!songTest.IsSongCorrect(_song))
            {
                return false;
            }
        }

        return true;
    }
}
