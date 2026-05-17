using UnityEngine.UI;
using UnityEngine;
public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image slotImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private ItemData currentItem;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.yellow;
    void Awake()
    {
        if (slotImage == null)
            slotImage = GetComponent<Image>();
        if (iconImage == null)
            iconImage = GetComponentInChildren<Image>();
    }
    public void ClearSlot()
    {
        currentItem = null;
        if (iconImage != null)
        {
            iconImage.sprite = null;
            iconImage.color = new Color(1, 0, 0, 0f);
        }
    }
    public void SetItem(ItemData item)
    {
        currentItem = item;
        if (iconImage != null && item != null && item.icon != null)
        {
            iconImage.sprite = item.icon;
            iconImage.color = Color.white;
        }
    }
    public void Select()
    {
        if (slotImage != null)
            slotImage.color = selectedColor;
    }
    public void Deselect()
    {
        if (slotImage != null)
            slotImage.color = normalColor;
    }
    public ItemData GetItem()
    {
        return currentItem;
    }
    public bool HasItem()
    {
        return currentItem != null;
    }
}