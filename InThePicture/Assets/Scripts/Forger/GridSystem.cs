using UnityEngine;

public class GridSystem : MonoBehaviour {
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;

    private bool[,] grid;

    void Start() {
        grid = new bool[width, height];
    }

    // Converte coordenadas da grid para posição no mundo
    public Vector2 GetWorldPosition(int x, int y) {
        return (Vector2)transform.position + new Vector2(x * cellSize, y * cellSize);
    }

    // Converte posição no mundo para coordenadas da grid
    public void GetXY(Vector2 worldPos, out int x, out int y) {
        Vector2 localPos = (worldPos - (Vector2)transform.position) / cellSize;
        x = Mathf.FloorToInt(localPos.x);
        y = Mathf.FloorToInt(localPos.y);
    }

    public bool IsInBounds(int x, int y) {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    public bool IsCellOccupied(int x, int y) {
        if (!IsInBounds(x, y)) return false;
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
            if (!IsInBounds(x, y) || IsCellOccupied(x, y))
                return false;
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
                if (!grid[x, y])
                    return false;
            }
        }
        return true;
    }

    void OnDrawGizmos() {
        Vector2 drawOrigin = (Vector2)transform.position;
        DrawGridLines(drawOrigin);
        DrawOccupiedCells(drawOrigin);
    }

    void DrawGridLines(Vector2 drawOrigin) {
        Gizmos.color = Color.gray;

        for (int x = 0; x <= width; x++) {
            Vector2 start = drawOrigin + new Vector2(x * cellSize, 0);
            Vector2 end = drawOrigin + new Vector2(x * cellSize, height * cellSize);
            Gizmos.DrawLine(start, end);
        }

        for (int y = 0; y <= height; y++) {
            Vector2 start = drawOrigin + new Vector2(0, y * cellSize);
            Vector2 end = drawOrigin + new Vector2(width * cellSize, y * cellSize);
            Gizmos.DrawLine(start, end);
        }
    }

    void DrawOccupiedCells(Vector2 drawOrigin) {
        if (grid == null) return;

        Gizmos.color = Color.red;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (grid[x, y]) {
                    Vector2 cellCenter = drawOrigin + new Vector2(x * cellSize, y * cellSize) + Vector2.one * (cellSize / 2f);
                    Gizmos.DrawCube(cellCenter, Vector3.one * cellSize * 0.9f);
                }
            }
        }
    }
}
