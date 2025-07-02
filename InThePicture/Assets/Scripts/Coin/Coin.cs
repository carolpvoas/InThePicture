using UnityEngine;
using UnityEngine.EventSystems;

public class Coin : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform uiPosition;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;

    public bool isPlaced = false;

    public int coinValue = 1; // Valor da moeda

    void Awake()
    {
        uiPosition = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        originalPosition = uiPosition.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        uiPosition.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (!isPlaced)
        {
            uiPosition.anchoredPosition = originalPosition;
        }
    }

    public void SnapToBox(Vector2 targetPosition)
    {
        uiPosition.anchoredPosition = targetPosition;
        isPlaced = true;
    }
}