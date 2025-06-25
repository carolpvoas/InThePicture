using UnityEngine;

public class GridSystem : MonoBehaviour {
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;
    public Vector2 origin = Vector2.zero;

    private bool[,] grid;

    
    void Awake() {
        grid = new bool[width, height];
    }

    public Vector2 GetWorldPosition(int x, int y) {
        return new Vector2(x, y) * cellSize + origin;
    }

    public void GetXY(Vector2 worldPos, out int x, out int y) {
        Vector2 local = (worldPos - origin) / cellSize;
        x = Mathf.FloorToInt(local.x);
        y = Mathf.FloorToInt(local.y);
    }

    public bool IsInBounds(int x, int y) {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    public bool IsCellOccupied(int x, int y) {
        return grid[x, y];
    }

    public void SetCell(int x, int y, bool occupied) {
        if (IsInBounds(x, y)) {
            grid[x, y] = occupied;
        }
    }

    public bool CanPlace(Vector2Int[] shape, int startX, int startY) {
        foreach (Vector2Int offset in shape) {
            int x = startX + offset.x;
            int y = startY + offset.y;
            if (!IsInBounds(x, y) || IsCellOccupied(x, y)) return false;
        }
        return true;
    }

    public void PlaceShape(Vector2Int[] shape, int startX, int startY) {
        foreach (Vector2Int offset in shape) {
            int x = startX + offset.x;
            int y = startY + offset.y;
            SetCell(x, y, true);
        }
    }
    public bool IsGridFull() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (!grid[x, y]) {
                    return false;
                }
            }
        }
        return true;
    }

    
    void OnDrawGizmos() {
        Gizmos.color = Color.gray;

        for (int x = 0; x <= width; x++) {
            Vector2 from = origin + new Vector2(x * cellSize, 0);
            Vector2 to = origin + new Vector2(x * cellSize, height * cellSize);
            Gizmos.DrawLine(from, to);
        }

        for (int y = 0; y <= height; y++) {
            Vector2 from = origin + new Vector2(0, y * cellSize);
            Vector2 to = origin + new Vector2(width * cellSize, y * cellSize);
            Gizmos.DrawLine(from, to);
        }
        if (grid != null) {
            Gizmos.color = Color.red;
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    if (grid[x, y]) {
                        Vector2 cellCenter = GetWorldPosition(x, y) + Vector2.one * (cellSize / 2f);
                        Gizmos.DrawCube(cellCenter, Vector3.one * cellSize * 0.9f);
                    }
                }
            }
        }
    }
}


