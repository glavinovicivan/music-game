using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToggleGameObjectEnabledQuestReward", menuName = "ScriptableObjects/QuestRewards/ToggleGameObjectEnabledQuestReward")]
public class ToggleGameObjectEnabledQuestReward : QuestReward
{
    public bool ToggleState;
    public override void ApplyReward(PlayerController _playerController, IInteractable _questgiverController)
    {
        QuestgiverControllerWorldObjectInteraction questGiverControllerWorldObjectInteraction = _questgiverController as QuestgiverControllerWorldObjectInteraction;

        if (questGiverControllerWorldObjectInteraction != null)
        {
            List<GameObject> gameObjects = questGiverControllerWorldObjectInteraction.GetObjects();
            foreach (GameObject _gameObject in gameObjects)
            {
                _gameObject.SetActive(ToggleState);
            }
        }

        else
        {
            Debug.Log("Interactable is not QuestgiverControllerWorldObjectInteraction!");
        }
    }
}
