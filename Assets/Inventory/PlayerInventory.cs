using System;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInventory : MonoBehaviour
{
    public event Action InventoryChanged;
    [Header("Inventory Settings")]
    [SerializeField] private int maxSlots = 24;
    private List<ItemData> items = new List<ItemData>();
    public bool AddItem(ItemData item)
    {
        if (item == null)
        {
            return false;
        }
        if (items.Count >= maxSlots)
        {
            return false;
        }
        items.Add(item);
        InventoryChanged?.Invoke();
        return true;
    }
    public bool RemoveItem(int itemId)
    {
        ItemData itemToRemove = null;
        foreach (var item in items)
        {
            if (item.itemId == itemId)
            {
                itemToRemove = item;
                break;
            }
        }
        if (itemToRemove != null)
        {
            items.Remove(itemToRemove);
            InventoryChanged?.Invoke();
            return true;
        }
        return false;
    }
    public bool HasItem(int itemId)
    {
        foreach (var item in items)
        {
            if (item.itemId == itemId)
                return true;
        }
        return false;
    }
    public ItemData GetItem(int index)
    {
        if (index >= 0 && index < items.Count)
            return items[index];
        return null;
    }
    public int GetItemCount()
    {
        return items.Count;
    }
    public List<ItemData> GetAllItems()
    {
        return new List<ItemData>(items);
    }
    public bool HasFreeSlot()
    {
        return items.Count < maxSlots;
    }
    public int FreeSlots()
    {
        return maxSlots - items.Count;
    }
}
