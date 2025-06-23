using UnityEngine;
using UnityEngine.EventSystems;

public class InfCoin : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform uiPosition;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;


    public GameObject CoinPref;

    void Awake()
    {
        uiPosition = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        GameObject coin = Instantiate(CoinPref);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}