using UnityEngine;

public class DragBoy : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 originalPosition;
    private bool dragging = false;
    
    public GameObject trabalhador;
    public DialogueSystem dialogueSystem;

    void Start()
    {
        originalPosition = transform.position;
    }
    
    void OnMouseDown()
    {
        Debug.Log("Criança clicada!");
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
        
//        if (!IsOverTrabalhador()) // Verifica se está em cima do trabalhador
//
//        {
//            transform.position = originalPosition; // Volta à posição original
//        }
        if (IsOverTrabalhador())
        {
            if (dialogueSystem != null)
            {
                dialogueSystem.StartForcedDialogue();
            }
            else
            {
                Debug.LogWarning("DialogueSystem não está ligado no Inspector!");
            }
        }
    
        // Volta à posição original quer esteja ou não em cima do trabalhador
        transform.position = originalPosition;
    }

    void Update()
    {
        if (dragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition + offset;
        }
    }

    bool IsOverTrabalhador()
    {
        if (trabalhador == null)
        {
            Debug.LogWarning("Trabalhador não está definido no Inspector!");
            return false;
        }

        Collider2D trabalhadorCollider = trabalhador.GetComponent<Collider2D>();
        if (trabalhadorCollider == null)
        {
            Debug.LogWarning("Trabalhador não tem Collider2D!");
            return false;
        }

        return trabalhadorCollider.OverlapPoint(transform.position);
        // Verifica se o ponto atual da criança está dentro do collider do trabalhador
    }

    void OnMouseOver()
    {
        Debug.Log("o rato está finalmente a detetar a criança ;-;");
    }
    
    public void ResetPosition()
    {
        dragging = false;        // para o drag
        transform.position = originalPosition;  // volta à posição original
    }
}