using UnityEngine;
using UnityEngine.EventSystems;

public class PayMachine : MonoBehaviour, IDropHandler
{
    public RectTransform snapPosition;
    public CoinPayManager manager;

    public void OnDrop(PointerEventData eventData)
    {
        Coin coin = eventData.pointerDrag.GetComponent<Coin>();
        if (coin != null && !coin.isPlaced)
        {
            Vector2 offsetPosition = snapPosition.anchoredPosition + manager.GetNextOffset();
            coin.SnapToBox(offsetPosition);
            manager.AddCoin();
        }
    }
}