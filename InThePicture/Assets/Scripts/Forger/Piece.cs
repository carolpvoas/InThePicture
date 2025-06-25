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

    void Start()
    {
        grid = FindObjectOfType<GridSystem>();
        originalPos = transform.position;
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
        Debug.Log("Tentando encaixar em: " + baseX + ", " + baseY);

        if (grid.CanPlace(shape, baseX, baseY))
        {
            Vector2 snapPos = grid.GetWorldPosition(baseX, baseY);
            transform.position = snapPos;
            grid.PlaceShape(shape, baseX, baseY);
            placed = true;
            
            if (grid.IsGridFull()) {
                Debug.Log("Vitória!");
                if (!string.IsNullOrEmpty(nextSceneName))
                    SceneManager.LoadScene(nextSceneName);
                else
                    Debug.LogWarning("Nome da próxima cena não está definido!");
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
}