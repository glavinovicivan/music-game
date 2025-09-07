using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestgiverControllerWorldObjectInteraction : QuestgiverController
{
    [SerializeField]
    private List<GameObject> m_gameobjects;
    public List<GameObject> GetObjects()
    {
        return m_gameobjects;
    }
}
