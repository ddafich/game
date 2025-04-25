using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator animator;
    public List<LootItem> lootTable = new List<LootItem>();
    bool isOpened = false;
    Rigidbody2D rb;

    
    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("open");
            if (isOpened == false)
            {
                foreach (LootItem lootItem in lootTable)
                {
                    if (UnityEngine.Random.Range(0f, 100f) <= lootItem.dropChance)
                    {
                        InstantiateLoot(lootItem.itemPrefab);
                        isOpened = true;
                    }
                }
            }
        }
    }
    private void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, transform.position, Quaternion.identity);
            rb = droppedLoot.GetComponent<Rigidbody2D>();
            if (rb)
            {
                Vector2 randomForce = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1.5f)); // Adjust force as needed
                rb.AddForce(randomForce, ForceMode2D.Impulse);
                rb.linearDamping = 2f;
            }
        }
    }
}
