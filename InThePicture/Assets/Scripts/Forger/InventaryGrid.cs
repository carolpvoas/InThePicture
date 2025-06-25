using UnityEngine;
using UnityEngine.UI;

public class InventoryGrid : MonoBehaviour
{
    public int gridWidth = 5;
    public int gridHeight = 5;
    public GameObject slotPrefab;
    private InventoryItem[,] grid;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        grid = new InventoryItem[gridWidth, gridHeight];

        GenerateGridVisual();
    }

    void GenerateGridVisual()
    {
        GridLayoutGroup layout = GetComponent<GridLayoutGroup>();
        layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layout.constraintCount = gridWidth;

        for (int i = 0; i < gridWidth * gridHeight; i++)
        {
            Instantiate(slotPrefab, transform);
        }
    }

    public bool CanPlaceItem(InventoryItem item, int x, int y)
    {
        if (x + item.width > gridWidth || y + item.height > gridHeight)
            return false;

        for (int ix = 0; ix < item.width; ix++)
        {
            for (int iy = 0; iy < item.height; iy++)
            {
                if (grid[x + ix, y + iy] != null)
                    return false;
            }
        }

        return true;
    }

    public bool PlaceItem(InventoryItem item, int x, int y)
    {
        if (!CanPlaceItem(item, x, y))
        {
            Debug.Log("Espaço insuficiente para: " + item.itemName);
            return false;
        }

        for (int ix = 0; ix < item.width; ix++)
        {
            for (int iy = 0; iy < item.height; iy++)
            {
                grid[x + ix, y + iy] = item;
            }
        }

        Debug.Log("Item adicionado: " + item.itemName + $" em ({x},{y})");
        return true;
    }
}