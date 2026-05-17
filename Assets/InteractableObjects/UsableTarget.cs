using UnityEngine;
using UnityEngine.Events;
public class UsableTarget : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData requiredItem;
    [SerializeField] private UnityEvent onSuccessUse;
    private bool used = false;
    public void Interact()
    {
        if (used)
        {
            return;
        }
        UIManager.Instance.ShowInventory();
    }

    public void TryUseItem(int itemId)
    {
        if (itemId == requiredItem.itemId)
        {
            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
            inventory?.RemoveItem(itemId);
            used = true;
            UIManager.Instance.HideInventory();
            onSuccessUse?.Invoke();
            if (used) Destroy(gameObject);
        }
        else
        {
            return;
        }
    }
    public string GetPromptAction() => "Использовать предмет";
    public string GetObjectName() => requiredItem?.itemName ?? "предмет";
}