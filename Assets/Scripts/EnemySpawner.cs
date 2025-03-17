using System.Collections;
using Unity.VisualScripting;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    

    public Transform[] spawnPoints;
    public int enemiesPerWave = 5;
    public float timeBetweenWaves = 5f;
    public float spawnDelay = 1f;
    public bool isCleared;

    private int maxWave = 3;
    private int waveNumber = 0;
    private int enemiesAlive = 0;
    private int enemiesSpawned = 0; // Tracks how many have spawned
    private bool isSpawning = false;

    DamageableChar DamageableChar;

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

        int totalEnemies = enemiesPerWave + waveNumber; // Increase difficulty

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
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject randEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
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
                if (enemiesAlive <= 0 && enemiesSpawned >= (enemiesPerWave + waveNumber) && !isSpawning)
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
