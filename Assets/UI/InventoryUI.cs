using System.Collections.Generic;
using UnityEngine;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform slotGrid;
    [Header("Inventory Settings")]
    [SerializeField] private int totalSlots = 24;
    private PlayerInventory playerInventory;
    private List<InventorySlot> slots = new List<InventorySlot>();
    private int selectedSlotIndex = 0;
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
            playerInventory.InventoryChanged += RefreshUI;
        GetSlotsFromScene();
        RefreshUI();
    }
    void GetSlotsFromScene()
    {
        slots.Clear();
        foreach (Transform child in slotGrid)
        {
            InventorySlot slot = child.GetComponent<InventorySlot>();
            if (slot != null)
            {
                slots.Add(slot);
            }
        }
    }
    void Update()
    {
        if (!UIManager.Instance.IsInventoryOpen()) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            selectedSlotIndex--;
            if (selectedSlotIndex < 0)
                selectedSlotIndex = totalSlots - 1;
            UpdateSelection();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            selectedSlotIndex++;
            if (selectedSlotIndex >= totalSlots)
                selectedSlotIndex = 0;
            UpdateSelection();
        }
        if (Input.GetKeyDown(KeyCode.E) && slots.Count > 0)
        {
            InventorySlot selectedSlot = slots[selectedSlotIndex];
            if (selectedSlot.HasItem())
            {
                PlayerInteraction player = FindObjectOfType<PlayerInteraction>();
                if (player != null && player.currentEObject != null)
                {
                    UsableTarget usableTarget = player.currentEObject as UsableTarget;
                    if (usableTarget != null)
                    {
                        usableTarget.TryUseItem(selectedSlot.GetItem().itemId);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.HideInventory();
        }
    }
    void RefreshUI()
    {
        List<ItemData> items = playerInventory.GetAllItems();
        foreach (InventorySlot slot in slots)
        {
            slot.ClearSlot();
        }
        for (int i = 0; i < items.Count && i < slots.Count; i++)
        {
            slots[i].SetItem(items[i]);
        }
        selectedSlotIndex = 0;
        UpdateSelection();
    }
    void UpdateSelection()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i == selectedSlotIndex)
                slots[i].Select();
            else
                slots[i].Deselect();
        }
    }
    public void OpenInventory()
    {
        UIManager.Instance.ShowInventory();
        RefreshUI();
    }
    public void CloseInventory()
    {
        UIManager.Instance.HideInventory();
    }
}