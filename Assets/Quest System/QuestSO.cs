using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestData", menuName = "Quests/Quest data")]
public class QuestSO : ScriptableObject
{
    public int Goal = 1;
    public string QuestName;
    public string QuestDescription;
    public ItemData TransferItem;
    public ItemData ItemReward;
    public int QuestId = -1;
    public List<QuestStage> Stages;

    public bool NeedReStartEvent = false;
    public bool HasStartAnimation = true;
    public bool HasFinishAnimation = true;
}