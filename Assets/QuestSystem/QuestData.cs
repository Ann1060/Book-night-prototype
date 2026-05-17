using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    [field: SerializeField] public int quest_id { get; private set; }
    [field: SerializeField] public string quest_name { get; private set; }
    [field: SerializeField] public string quest_description { get; private set; }
    [field: SerializeField] public int goal { get; private set; }
    [field: SerializeField] public ItemData TransferItem { get; private set; }
    [field: SerializeField] public ItemData ItemReward { get; private set; }
    [field: SerializeField] public bool NeedReStartEvent { get; private set; }
    [field: SerializeField] public bool HasStartAnimation { get; private set; }
    [field: SerializeField] public bool HasFinishAnimation { get; private set; }
    public List<QuestStage> stages;
    public int currentStageIndex;
    public bool active;
    public bool finished;
    public QuestStage CurrentStage =>
        (stages != null && currentStageIndex < stages.Count)
        ? stages[currentStageIndex]
        : null;
    public QuestData(int goal, string name, string desc, int id, ItemData transferItem, ItemData itemReward, 
        bool needRestartEvent, bool hasStartAnimation, bool hasFinishAnimation, List<QuestStage> stages)
    {
        this.goal = goal;
        quest_name = name;
        quest_description = desc;
        quest_id = id;
        TransferItem = transferItem;
        ItemReward = itemReward;
        NeedReStartEvent = needRestartEvent;
        HasStartAnimation = hasStartAnimation;
        HasFinishAnimation = hasFinishAnimation;

        this.stages = new List<QuestStage>();

        foreach (var s in stages)
        {
            this.stages.Add(new QuestStage
            {
                step = s.step,
                targetId = s.targetId,
                dialogNpcId = s.dialogNpcId,
                goal = s.goal,
                dialogIndexOnEnter = s.dialogIndexOnEnter,
                progress = 0
            });
        }
        currentStageIndex = 0;
        active = false;
        finished = false;
    }
    public static QuestData FromSO(QuestSO so)
    {
        return new QuestData(
            goal: so.Goal,
            name: so.QuestName,
            desc: so.QuestDescription,
            id: so.QuestId,
            transferItem: so.TransferItem,
            itemReward: so.ItemReward,
            needRestartEvent: so.NeedReStartEvent,
            hasStartAnimation: so.HasStartAnimation,
            hasFinishAnimation: so.HasFinishAnimation,
            stages: so.Stages
        );
    }
}