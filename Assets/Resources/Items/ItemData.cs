using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public int itemId;
    public string itemName;
    public Sprite icon;
    public bool isUsable = true;
}