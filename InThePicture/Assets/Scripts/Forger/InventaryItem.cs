using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public int width;
    public int height;
    public Sprite icon;

    public InventoryItem(string name, int width, int height, Sprite icon)
    {
        this.itemName = name;
        this.width = width;
        this.height = height;
        this.icon = icon;
    }
}