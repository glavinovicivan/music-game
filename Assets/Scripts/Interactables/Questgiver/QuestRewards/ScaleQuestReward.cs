using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScaleQuestReward", menuName = "ScriptableObjects/QuestRewards/ScaleQuestReward")]
public class ScaleQuestReward : QuestReward
{
    public List<ScaleSO> Scales;
    public override void ApplyReward(PlayerController _playerController, IInteractable _questgiverController)
    {
        foreach (ScaleSO scale in Scales)
        {
            _playerController.AddScale(scale);
        }
    }
}
