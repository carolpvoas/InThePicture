using UnityEngine;

public class GearMiniGame : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    
    public Transform slotPosition; // Define no inspector
    public GearMiniGame[] gears;


    void OnMouseDown()
    {
        isDragging = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePos;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Aqui podemos verificar se a engrenagem está perto do slot correto
        CheckIfInSlot();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos + offset;
        }
        
        bool allPlaced = true;
        foreach (var gear in gears)
        {
            //if (!gear.placedCorrectly)
            {
                allPlaced = false;
                break;
            }
        }
    
        if (allPlaced)
        {
            Debug.Log("Mini game completo!");
            // Passa para a cena do próximo nível
            UnityEngine.SceneManagement.SceneManager.LoadScene("ProximoNivel");
        }
    }

    void CheckIfInSlot()
    {
        // Lógica para verificar proximidade com o slot correto
        // Exemplo: se estiver dentro de X unidades do slot, "encaixa"
        
        float distance = Vector3.Distance(transform.position, slotPosition.position);
        if (distance < 0.5f) // Ajusta o valor conforme o tamanho do teu sprite
        {
            transform.position = slotPosition.position;
            // Marca a engrenagem como “colocada”
            //placedCorrectly = true;
            // Talvez desativa o drag para essa engrenagem
            isDragging = false;
        }
    }
}