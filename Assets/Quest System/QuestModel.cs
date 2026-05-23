using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class QuestModel : MonoBehaviour
{
    public List<QuestData> activeQuests { get; private set; }
    public List<QuestData> allQuests { get; private set; }
    [SerializeField] private QuestSO[] _questList;
    private void Awake()
    {
        activeQuests = new List<QuestData>();
        allQuests = new List<QuestData>();
        foreach (var so in _questList)
        {
            if (allQuests.Any(q => q.quest_id == so.QuestId))
            {
                continue;
            }
            allQuests.Add(QuestData.FromSO(so));
        }
    }
    public QuestData GetQuest(int id)
    {
        return allQuests.Find(q => q.quest_id == id);
    }
    public QuestData GetActiveQuest(int id)
    {
        return activeQuests.Find(q => q.quest_id == id);
    }
}