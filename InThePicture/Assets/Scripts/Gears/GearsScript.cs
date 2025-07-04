using UnityEngine;
using UnityEngine.EventSystems;

public class GearsScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum GearType { Small, Medium, Large }
    [Header("Configuração da engrenagem")]
    public GearType gearType;

    [Tooltip("Distância máxima para snap")]
    public float snapDistance = 150f;

    [Tooltip("Slots válidos para esta engrenagem")]
    public RectTransform[] validSlots;

    [HideInInspector] public RectTransform currentSlot = null;
    [HideInInspector] public bool placedCorrectly = false;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;

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

        float closestDistance = Mathf.Infinity;
        RectTransform closestSlot = null;

        foreach (RectTransform slot in validSlots)
        {
            float dist = Vector2.Distance(rectTransform.anchoredPosition, slot.anchoredPosition);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestSlot = slot;
            }
        }

        if (closestSlot != null && closestDistance < snapDistance)
        {
            rectTransform.anchoredPosition = closestSlot.anchoredPosition;
            currentSlot = closestSlot;
            placedCorrectly = true;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            rectTransform.anchoredPosition = initialPosition;
            currentSlot = null;
            placedCorrectly = false;
        }
    }

    public bool IsPlacedCorrectly()
    {
        return placedCorrectly && currentSlot != null;
    }
}
