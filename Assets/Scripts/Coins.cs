using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public int amount { get; private set; }
    [SerializeField]
    private TextMeshProUGUI amountText;
    [SerializeField]
    private Transform spawnTransform;
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private Image coinPrefab;
    [SerializeField]
    private Canvas canvas;
    private Vector3 startTextPosition;

    private void Start()
    {
        startTextPosition=amountText.transform.position;
        UpdateUI();
    }

    private void OnEnable()
    {
        ItemsCollector.ItemSold += ItemsCollector_ItemSold;
    }

    private void OnDisable()
    {
        ItemsCollector.ItemSold -= ItemsCollector_ItemSold;
    }

    private void ItemsCollector_ItemSold(object sender, ItemSoldEventArgs e)
    {
        amount += e.cost;
        FlyingCoin();
    }

    private void UpdateUI()
    {
        var currentAmount= int.Parse(amountText.text);
        DOTween.To(() => currentAmount, x => currentAmount = x, amount, 1f)
            .OnUpdate(() => { amountText.text = currentAmount.ToString(); });
        amountText.transform.DOShakePosition(1f, 5).OnComplete(()=>amountText.transform.position=startTextPosition);
    }

    private void FlyingCoin()
    {
        var CoinTransform = Instantiate(coinPrefab,canvas.transform).GetComponent<Image>();
        CoinTransform.transform.position = Camera.main.WorldToScreenPoint(spawnTransform.position);
        CoinTransform.transform.DOScale(1, 0.2f).SetDelay(0.5f);
        CoinTransform.transform.DOMove(targetTransform.position, 0.6f).OnComplete(() => { targetTransform.DOShakeScale(0.1f,1); Destroy(CoinTransform.gameObject); UpdateUI(); }).SetDelay(0.6f);
    }
}
