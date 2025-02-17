using System;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    public int coinAmount = 1;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            CoinUI.instance.AddCoin(coinAmount);
        }
    }
}
