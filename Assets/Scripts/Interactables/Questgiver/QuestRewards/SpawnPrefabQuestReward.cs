using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPrefabQuestReward", menuName = "ScriptableObjects/QuestRewards/SpawnPrefabQuestReward")]
public class SpawnPrefabQuestReward : QuestReward
{
    public GameObject Prefab;
    public Vector3 Location;
    public override void ApplyReward(PlayerController _playerController, IInteractable _questgiverController)
    {
        Instantiate(Prefab, Location, Quaternion.identity);
    }
}
