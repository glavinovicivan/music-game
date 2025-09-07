using UnityEngine;

public abstract class QuestReward : ScriptableObject
{
    public abstract void ApplyReward(PlayerController _playerController, IInteractable _questgiverController);
}
