using DG.Tweening;
using TMPro;
using UnityEngine;

public class ItemsCollectorUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI amountOfItemsText;
    [SerializeField]
    private Color maxCapacityColor;
    [SerializeField]
    private float duration=0.05f;
    private void OnEnable()
    {
        ItemsCollector.OnNumberOfItemsChanged += ItemsCollector_ItemPicked;
        ItemsCollector.MaxCapacityReached += ItemsCollector_MaxCapacityReached;
    }
    private void OnDisable()
    {
        ItemsCollector.OnNumberOfItemsChanged -= ItemsCollector_ItemPicked;
        ItemsCollector.MaxCapacityReached -= ItemsCollector_MaxCapacityReached;
    }

    private void ItemsCollector_MaxCapacityReached(object sender, System.EventArgs e)
    {
        amountOfItemsText.DOColor(maxCapacityColor, duration).SetEase(Ease.Linear).OnComplete(() => ResetTextColor());
    }

    private void ItemsCollector_ItemPicked(object sender, ItemPickedEventArgs e)
    {
        amountOfItemsText.text=e.currentAmount+"/"+e.maxCapacity;
    }

    private void ResetTextColor()
    {
        amountOfItemsText.color = Color.white;
    }
}
