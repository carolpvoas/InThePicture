using UnityEngine;

public class TesteDrag : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging = false;
    void OnMouseDown()
    {
        Debug.Log("Criança clicada!");
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }
    void OnMouseOver()
    {
        Debug.Log("Rato está em cima da criança");
    }
}
