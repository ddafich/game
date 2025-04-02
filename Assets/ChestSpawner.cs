using System;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject Chest;
    public Transform ChestSpawnPos;
    public EnemySpawner enemySpawner;

    private void OnEnable()
    {
        enemySpawner.OnAreaCleared += SpawnChest;
    }
    private void OnDisable()
    {
        enemySpawner.OnAreaCleared -= SpawnChest;
    }
    private void SpawnChest()
    {
        Instantiate(Chest, ChestSpawnPos.position, Quaternion.identity);
    }
}
