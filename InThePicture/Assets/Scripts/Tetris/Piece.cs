using UnityEngine;
using UnityEngine.SceneManagement;

public class Piece : MonoBehaviour
{
    public Vector2Int[] shape; // Células relativas à origem (pivot)
    public float cellSize = 1f;

    private Vector3 dragOffset;
    private Vector3 originalPos;
    private GridSystem grid;
    public string nextSceneName;

    private bool placed = false;
    
    private Vector3 startPos;


    void Start()
    {
        grid = FindObjectOfType<GridSystem>();
        originalPos = transform.position;
        startPos = transform.position;

    }

    
    void OnMouseDown()
    {
        if (placed) return;
        dragOffset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        if (placed) return;
        transform.position = GetMouseWorldPos() + dragOffset;
    }

    void OnMouseUp()
    {
        if (placed) return;

        grid.GetXY(transform.position, out int baseX, out int baseY);
        Debug.Log("Tá a encaixar em: " + baseX + ", " + baseY);

        if (grid.CanPlace(shape, baseX, baseY))
        {
            Vector2 snapPos = grid.GetWorldPosition(baseX, baseY) + Vector2.one * (grid.cellSize / 2f);
            transform.position = snapPos;
            grid.PlaceShape(shape, baseX, baseY);
            placed = true;
            
            if (grid.IsGridFull()) {
                //GameProgressManager.Instance.UnlockCharacter("Basket Lady");
                //GameProgressManager.Instance.UnlockCharacter("Indigenous Woman");
                SceneManager.LoadScene(nextSceneName);
            }

        }
        else
        {
            transform.position = originalPos;
        }
    }


    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        return mousePos;
    }

    public void ResetPosition()
    {
        transform.position = startPos;
        placed = false;
    }

}