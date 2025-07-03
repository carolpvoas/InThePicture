using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public GridSystem gridSystem;
    public Piece[] allPieces;

    public void ResetPuzzle()
    {
        gridSystem.ClearGrid();

        foreach (Piece piece in allPieces)
        {
            piece.ResetPosition();
        }
    }
    
    public void ClearGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = false;
            }
        }
    }
}