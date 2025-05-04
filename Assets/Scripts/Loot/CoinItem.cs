using UnityEngine;

public class CoinItem : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public int coinAmount = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            CoinUI.instance.AddCoin(coinAmount);
            audioManager.PlaySFX(audioManager.coinPickup);
        }
    }
}
    