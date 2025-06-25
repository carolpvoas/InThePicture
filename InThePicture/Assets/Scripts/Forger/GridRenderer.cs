using UnityEngine;

public class GridRenderer : MonoBehaviour {
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;
    public Vector2 origin = Vector2.zero;
    public GameObject cellPrefab;

    void Start() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Vector2 pos = origin + new Vector2(x * cellSize, y * cellSize);
                Instantiate(cellPrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}