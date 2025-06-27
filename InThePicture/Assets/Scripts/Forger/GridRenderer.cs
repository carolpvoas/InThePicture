using UnityEngine;

public class GridRenderer : MonoBehaviour {
    public GameObject cellPrefab;

    private GridSystem gridSystem;

    void Start() 
    {
        gridSystem = FindObjectOfType<GridSystem>();
        if (gridSystem == null) {
            Debug.LogError("GridSystem não encontrado na cena!");
            return;
        }

        for (int x = 0; x < gridSystem.width; x++) 
        {
            for (int y = 0; y < gridSystem.height; y++) 
            {
                Vector2 pos = gridSystem.GetWorldPosition(x, y) + Vector2.one * (gridSystem.cellSize / 2f);
                Instantiate(cellPrefab, pos, Quaternion.identity, transform);
            }
        }
    }

    void OnDrawGizmos() 
    {
        if (gridSystem == null) {
            gridSystem = FindObjectOfType<GridSystem>();
            if (gridSystem == null) return;
        }

        Gizmos.color = Color.gray;

        for (int x = 0; x <= gridSystem.width; x++) 
        {
            Vector2 from = gridSystem.GetWorldPosition(x, 0);
            Vector2 to = gridSystem.GetWorldPosition(x, gridSystem.height);
            Gizmos.DrawLine(from, to);
        }

        for (int y = 0; y <= gridSystem.height; y++) 
        {
            Vector2 from = gridSystem.GetWorldPosition(0, y);
            Vector2 to = gridSystem.GetWorldPosition(gridSystem.width, y);
            Gizmos.DrawLine(from, to);
        }
    }
}