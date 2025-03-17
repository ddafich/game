using System;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorLock : MonoBehaviour
{
    public Collider2D[] m_Collider;
    public EnemySpawner EnemySpawner;

    [Header("Tilemap Settings")]
    public Tilemap tilemap;
    private void Start()
    {
        m_Collider = GetComponents<Collider2D>();
        SetColliderEnabled(false);
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
        if (EnemySpawner.isCleared)
        {
            tilemap.gameObject.SetActive(false);
            SetColliderEnabled(false);
        }
    }
    
}
