using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstrumentQuestReward", menuName = "ScriptableObjects/QuestRewards/InstrumentQuestReward")]
public class InstrumentQuestReward : QuestReward
{
    public List<InstrumentSO> Instruments;

    public override void ApplyReward(PlayerController _playerController, IInteractable _questgiverController)
    {
        foreach (InstrumentSO instrument in Instruments)
        {
            _playerController.AddInstrument(instrument);
        }
    }
}
