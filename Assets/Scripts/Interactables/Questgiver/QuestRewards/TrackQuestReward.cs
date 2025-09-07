using UnityEngine;

[CreateAssetMenu(fileName = "TrackQuestReward", menuName = "ScriptableObjects/QuestRewards/TrackQuestReward")]
public class TrackQuestReward : QuestReward
{
    public int TrackNumber;

    public override void ApplyReward(PlayerController _playerController, IInteractable _questgiverController)
    {
        _playerController.IncreaseTrackCount(TrackNumber);
    }
}
