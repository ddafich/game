using System;

using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorLock : MonoBehaviour
{
    public Collider2D[] m_Collider;
    public EnemySpawner EnemySpawner;
    AudioManager audioManager;

    [Header("Tilemap Settings")]
    public Tilemap tilemap;

    private bool hasOpened = false;
    private void Start()
    {
        m_Collider = GetComponents<Collider2D>();
        SetColliderEnabled(false);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    public void SetColliderEnabled(bool v)
    {
        foreach(var col in m_Collider)
        {
            col.enabled = v;
        }
    }
    private void Update()
    {
        if (EnemySpawner.isCleared&& !hasOpened)
        {
            tilemap.gameObject.SetActive(false);
            SetColliderEnabled(false);
            audioManager.PlaySFX(audioManager.doorOpen);
            hasOpened = true;
        }
    }
    
}
