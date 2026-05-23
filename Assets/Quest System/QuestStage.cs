using UnityEngine;
public enum TypeStep
{
    PickUpItem,
    TalkNPC
}
[System.Serializable]
public class QuestStage
{
    public TypeStep step;
    public int targetId;
    public int dialogNpcId;
    public int goal = 1;
    public int dialogIndexOnEnter = -1;
    [HideInInspector] public int progress = 0;
}