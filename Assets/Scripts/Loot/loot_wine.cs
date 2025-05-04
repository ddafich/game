using UnityEngine;

public class loot_wine : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public int increaseAmount;
    
    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SwordAttack swordAttack = col.gameObject.GetComponentInChildren<SwordAttack>();
            if (swordAttack != null)
            {
                swordAttack.IncreaseDamage(increaseAmount);
                
            }
            Destroy(gameObject);
            audioManager.PlaySFX(audioManager.powerUp);
        } 
    }
}
