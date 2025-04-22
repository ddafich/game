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
}
