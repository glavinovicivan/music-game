using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScaleSO", menuName = "ScriptableObjects/ScaleSO")]
public class ScaleSO : ScriptableObject
{
    public AudioClip ScaleClip;
    public List<ETones> Tones;

    [SerializeField]
    [HideInInspector]
    private int m_tonesSize;

    private void OnValidate()
    {
        List<ETones> cleanedToneList = new List<ETones>();
        foreach (ETones tone in Tones)
        {
            if (!cleanedToneList.Contains(tone))
            {
                cleanedToneList.Add(tone);
            }
        }

        if (m_tonesSize < Tones.Count)
        {
            foreach (ETones enumValue in Enum.GetValues(typeof(ETones)))
            {
                if (!cleanedToneList.Contains(enumValue))
                {
                    cleanedToneList.Add(enumValue);
                    break;
                }
            }
        }
        
        Tones = cleanedToneList;
        m_tonesSize = Tones.Count;
    }
}
