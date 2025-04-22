using UnityEngine;

public class area : MonoBehaviour
{
    public EnemySpawner EnemySpawner;
    public DoorLock DoorLock;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemySpawner.StartSpawning();
            DoorLock.SetColliderEnabled(true);
            DoorLock.tilemap.gameObject.SetActive(true);
            Collider2D areaCollider = GetComponent<Collider2D>();
            areaCollider.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
