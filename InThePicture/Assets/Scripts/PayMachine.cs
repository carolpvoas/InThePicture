using UnityEngine;
using UnityEngine.EventSystems;

public class PayMachine : MonoBehaviour, IDropHandler
{
    public RectTransform snapPosition;
    public CoinPayManager manager;

    public void OnDrop(PointerEventData eventData)
    {
        Coin coin = eventData.pointerDrag.GetComponent<Coin>();
        if (coin != null && !coin.placed)
        {
            coin.SnapToBox(snapPosition.anchoredPosition);
            manager.AddCoin();
        }
    }
}