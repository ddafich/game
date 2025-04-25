using UnityEngine;

public class loot_wine : MonoBehaviour
{
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
        }
        popup popup = FindObjectOfType<popup>();
        if (popup != null)
        {
            popup.ShowMessage("You feel stronger!");
        }
    }
}
