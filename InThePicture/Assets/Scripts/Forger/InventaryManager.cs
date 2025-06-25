using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryGrid inventoryGrid;
    public Sprite swordSprite;
    public Sprite potionSprite;

    void Start()
    {
        // Exemplo de criação de itens
        InventoryItem sword = new InventoryItem("Sword", 1, 3, swordSprite);
        InventoryItem potion = new InventoryItem("Potion", 1, 1, potionSprite);
        InventoryItem shield = new InventoryItem("Shield", 2, 2, null);

        inventoryGrid.PlaceItem(sword, 0, 0);    // Ocupa 1x3
        inventoryGrid.PlaceItem(potion, 2, 0);   // Ocupa 1x1
        inventoryGrid.PlaceItem(shield, 3, 1);   // Ocupa 2x2
    }
}