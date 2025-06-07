using UnityEngine;
using UnityEngine.EventSystems;

public class GearsScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform slotPosition;
    public float snapDistance = 150f;
    public bool placedCorrectly = false;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 initialPosition; // <- Salvar a posição inicial

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (placedCorrectly) return;

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;

        // Salva a posição inicial quando começa a arrastar
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (placedCorrectly) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (placedCorrectly) return;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        float distance = Vector2.Distance(rectTransform.anchoredPosition, slotPosition.anchoredPosition);

        if (distance < snapDistance)
        {
            rectTransform.anchoredPosition = slotPosition.anchoredPosition;
            placedCorrectly = true;
        }
        else
        {
            // Volta para a posição inicial se falhar
            rectTransform.anchoredPosition = initialPosition;
            placedCorrectly = false;
        }
    }
}