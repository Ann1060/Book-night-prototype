using System.Collections.Generic;
using UnityEngine;
public class QuestPresenter : MonoBehaviour
{
    [SerializeField] private QuestModel _model;
    public void StartQuest(QuestSO so)
    {
        var quest = _model.GetQuest(so.QuestId);
        if (quest == null)
        {
            return;
        }
        if (quest.active || quest.finished)
        {
            return;
        }
        quest.active = true;
        _model.activeQuests.Add(quest);
        CheckCurrentStageComplete(quest);
        StartStage(quest);
    }
    public void ProcessEvent(TypeStep step, int targetId, int amount = 1)
    {
        foreach (var quest in new List<QuestData>(_model.activeQuests))
        {
            if (!quest.active || quest.finished)
                continue;

            var stage = quest.CurrentStage;

            if (stage == null)
                continue;

            if (stage.step != step)
                continue;

            if (stage.targetId != targetId)
                continue;

            stage.progress += amount;
            if (stage.progress >= stage.goal)
            {
                AdvanceStage(quest);
            }
        }
    }
    private void StartStage(QuestData quest)
    {
        var stage = quest.CurrentStage;
        if (stage == null)
            return;

        stage.progress = 0;
        if (stage.dialogIndexOnEnter >= 0)
        {
            var npcs = FindObjectsOfType<NPC>();
            foreach (var npc in npcs)
            {
                if (npc.GetNpcId() == stage.dialogNpcId)
                {
                    npc.SetDialogIndex(stage.dialogIndexOnEnter);
                }
            }
        }
        CheckCurrentStageComplete(quest);
    }
    private void AdvanceStage(QuestData quest)
    {
        quest.currentStageIndex++;
        if (quest.currentStageIndex >= quest.stages.Count)
        {
            CompleteQuest(quest);
        }
        else
        {
            StartStage(quest);
        }
    }
    public void CheckCurrentStageCompletePublic(QuestSO so)
    {
        var quest = _model.GetQuest(so.QuestId);
        CheckCurrentStageComplete(quest);
    }
    private void CheckCurrentStageComplete(QuestData quest)
    {
        if (!quest.active)
            return;

        var stage = quest.CurrentStage;
        if (stage == null)
            return;

        var inventory = FindObjectOfType<PlayerInventory>();
        if (inventory == null)
            return;

        if (stage.step == TypeStep.PickUpItem)
        {
            if (quest.TransferItem != null &&
                inventory.HasItem(quest.TransferItem.itemId))
            {
                stage.progress = stage.goal;
                AdvanceStage(quest);
            }
        }
    }
    public void CompleteQuestUnityEvent(QuestSO so)
    {
        var quest = _model.GetQuest(so.QuestId);
        CompleteQuest(quest);
    }
    private void CompleteQuest(QuestData quest)
    {
        var inventory = FindObjectOfType<PlayerInventory>();
        if (inventory != null)
        {
            if (quest.TransferItem != null)
            {
                inventory.RemoveItem(quest.TransferItem.itemId);
            }
            if (quest.ItemReward != null)
            {
                inventory.AddItem(quest.ItemReward);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
        quest.finished = true;
        quest.active = false;
        _model.activeQuests.Remove(quest);
    }
}