using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static int gridWidth = 4;
    public static int gridHeight = 4;

    private Transform[,] grid = new Transform[gridWidth, gridHeight];

    public bool IsValidPosition(Vector2Int[] localPositions, Vector2 origin)
    {
        foreach (var pos in localPositions)
        {
            int x = Mathf.RoundToInt(origin.x + pos.x);
            int y = Mathf.RoundToInt(origin.y + pos.y);

            if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight || grid[x, y] != null)
                return false;
        }

        return true;
    }

    public void PlacePiece(Vector2Int[] localPositions, Transform piece, Vector2 origin)
    {
        foreach (var pos in localPositions)
        {
            int x = Mathf.RoundToInt(origin.x + pos.x);
            int y = Mathf.RoundToInt(origin.y + pos.y);
            grid[x, y] = piece;
        }
    }

    public bool IsGridFull()
    {
        foreach (var cell in grid)
        {
            if (cell == null)
                return false;
        }
        return true;
    }
}