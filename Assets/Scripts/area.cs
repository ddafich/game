using UnityEngine;

public class area : MonoBehaviour
{
    public EnemySpawner EnemySpawner;
    public DoorLock DoorLock;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemySpawner.StartSpawning();
            DoorLock.SetColliderEnabled(true);
            DoorLock.tilemap.gameObject.SetActive(true);
            audioManager.PlaySFX(audioManager.doorClose);
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
