/*using UnityEngine;

public class Piece : MonoBehaviour
{
    public Vector2Int[] localPositions;
    private Vector3 originalPosition;
    private Grid grid;

    private void Start()
    {
        originalPosition = transform.position;
        grid = FindObjectOfType<Grid>();
    }

    private void OnMouseDown()
    {
        originalPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = mousePos;
    }

    private void OnMouseUp()
    {
        Vector2 snappedPosition = new Vector2(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y)
        );

        if (grid.IsValidPosition(localPositions, snappedPosition))
        {
            transform.position = snappedPosition;
            grid.PlacePiece(localPositions, transform, snappedPosition);

            if (grid.IsGridFull())
            {
                Debug.Log("Puzzle completo. Ativar a próxima cena.");
                // loadscenae
            }
        }
        else
        {
            transform.position = originalPosition;
        }
    }
}*/