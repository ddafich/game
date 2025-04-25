using System;
using UnityEngine;

public class loot_chicken : MonoBehaviour
{
    public int healAmount = 1;
    public static event Action<int> OnHealthCollect;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (OnHealthCollect != null)
            {
                foreach (Delegate d in OnHealthCollect.GetInvocationList())
                {
                    var target = d.Target as UnityEngine.Object;
                    if (target != null)  // Check if the target still exists
                    {
                        d.DynamicInvoke(healAmount);
                    }
                }
            }
            DamageableChar damageableChar = col.gameObject.GetComponent<DamageableChar>();
            if(damageableChar != null)
            {
                damageableChar.IncreaseMaxHealth(healAmount);
            }
            popup popup = FindObjectOfType<popup>();
            if (popup != null)
            {
                popup.ShowMessage("You feel stronger!");
            }
            Destroy(gameObject);
        }
    }
}
