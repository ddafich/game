using System.Collections.Generic;
using UnityEngine;

public class DetectionCircle : MonoBehaviour
{
    public string tagTarget = "Player";
    public List<Collider2D> detectedObjects = new List<Collider2D>();
    public Collider2D col;
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagTarget)
        {
            detectedObjects.Add(collider);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedObjects.Remove(collision);
    }
}
