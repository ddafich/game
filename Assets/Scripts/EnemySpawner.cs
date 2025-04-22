using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;


    public Transform[] spawnPoints;
    public int enemiesPerWave = 0;
    public float timeBetweenWaves = 5f;
    public float spawnDelay = 1f;
    public bool isCleared;
    private bool hasCleared = false;

    public int maxWave = 3;
    private int waveNumber = 0;
    public int enemiesAlive = 0;
    public int enemiesSpawned = 0; // Tracks how many have spawned
    private bool isSpawning = false;

    

    DamageableChar DamageableChar;
    public event Action OnAreaCleared;
    private void Update()
    {
        if (isCleared && !hasCleared)
        {
            hasCleared = true;
            OnAreaCleared?.Invoke();
        }
    }
    public void StartSpawning()
    {
        if (!isSpawning)
        {
            StartCoroutine(StartWave());
        }
    }
    IEnumerator StartWave()
    {
        isSpawning = true;
        isCleared = false;
        waveNumber++;
        enemiesAlive = 0; // Reset count to be updated properly
        enemiesSpawned = 0;
        int totalEnemies = enemiesPerWave + waveNumber -1; // Increase difficulty
        Debug.Log("Starting Wave " + waveNumber + " with " + totalEnemies + " enemies");
        for (int i = 0; i < totalEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
        isSpawning = false;
    }
    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        GameObject randEnemy = enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)];
        GameObject enemy = Instantiate(randEnemy, spawnPoint.position, Quaternion.identity);
        enemiesAlive++; // Only increase when actually spawning an enemy
        enemiesSpawned++;

        DamageableChar enemyHealth = enemy.GetComponent<DamageableChar>();
        if (enemyHealth != null)
        {
            enemyHealth.OnDeath += () =>
            {
                enemiesAlive--;
                Debug.Log("Enemy died. Enemies left: " + enemiesAlive);

                // Only start next wave if all enemies have spawned and died
                if (enemiesAlive == 0 && waveNumber <= maxWave && !isSpawning)
                {
                    StartCoroutine(WaitForNextWave());
                }
            };
        }
    }
    IEnumerator WaitForNextWave()
    {
        Debug.Log("Wave " + waveNumber + " completed. Next wave in " + timeBetweenWaves + " seconds...");
        if (waveNumber < maxWave)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            StartCoroutine(StartWave());
        }
        else
        {
            isCleared = true;
            Debug.Log("area cleared");
        }
    } 
}
