using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Dialogue dialogue;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            dialogue.TriggerDialogue();
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            dialogue.TriggerDialogue();
            Destroy(gameObject);
        }
    }
}
