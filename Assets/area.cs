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
            DoorLock.ChangeTiles(DoorLock.doorClose);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D areaCollider = GetComponent<Collider2D>();
            areaCollider.enabled = false;
        }
    }
}
