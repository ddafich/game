using System;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    public int coinAmount = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            CoinUI.instance.AddCoin(coinAmount);
        }
    }
}
    