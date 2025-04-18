using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public static CoinUI instance;
    public TMP_Text coinText;
    public int currentCoin = 0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        coinText.text = " " + currentCoin.ToString();
    }
    public void AddCoin(int amount)
    {
        currentCoin += amount;
        coinText.text = " " + currentCoin.ToString();
    }
}
