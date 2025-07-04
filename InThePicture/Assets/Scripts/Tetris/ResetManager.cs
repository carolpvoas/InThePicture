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
}