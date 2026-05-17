using UnityEngine;
public class PickupItem : MonoBehaviour, IInteractable
{
    public int itemId;
    [SerializeField] private ItemData itemData;
    [SerializeField] private bool destroyOnPickup = true;
    [SerializeField] private QuestSO questSO;
    public virtual void Interact()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory != null)
        {
            if (inventory.HasFreeSlot())
            {
                if (inventory.AddItem(itemData))
                {
                    if (destroyOnPickup)
                    {
                        Destroy(gameObject);
                    }
                }
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
        QuestPresenter questPresenter = FindObjectOfType<QuestPresenter>();
        questPresenter.CheckCurrentStageCompletePublic(questSO);
    }
    public string GetPromptAction() => "Взять";
    public string GetObjectName() => itemData.itemName;
    public int GetItemId() => itemId;
    public ItemData GetItemData() => itemData;
}