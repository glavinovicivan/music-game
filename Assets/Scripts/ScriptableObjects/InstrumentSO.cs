using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstrumentSO", menuName = "ScriptableObjects/InstrumentSO")]
public class InstrumentSO : ScriptableObject
{
    public string InstrumentName;
    public List<ToneSoundPairing> ToneSoundPairings;

    [SerializeField]
    [HideInInspector]
    private int m_tonesSize;

    private void OnValidate()
    {
        List<ToneSoundPairing> cleanedToneList = new List<ToneSoundPairing>();
        foreach (ToneSoundPairing tone in ToneSoundPairings)
        {
            if (!cleanedToneList.Contains(tone))
            {
                cleanedToneList.Add(tone);
            }
        }

        if (m_tonesSize < ToneSoundPairings.Count)
        {
            foreach (ETones enumValue in Enum.GetValues(typeof(ETones)))
            {
                List<ToneSoundPairing> matchingTonePairings = cleanedToneList.FindAll(x => x.Tone == enumValue);
                if (matchingTonePairings.Count == 0)
                {
                    cleanedToneList.Add(new ToneSoundPairing() { Tone = enumValue });
                    break;
                }
            }
        }

        ToneSoundPairings = cleanedToneList;
        m_tonesSize = ToneSoundPairings.Count;
    }
}
