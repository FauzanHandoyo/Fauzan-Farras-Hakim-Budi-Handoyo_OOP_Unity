using System.Collections.Generic; // Required for List
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Enemy Spawners")]
    public EnemySpawner[] enemySpawners;

    [Header("Wave System")]
    public float timer = 0f;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    private List<Enemy> activeEnemies = new List<Enemy>(); // List to track active enemies

    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waveInterval && totalEnemies <= 0)
        {
            StartWave();
            timer = 0f;
        }
    }

    private void StartWave()
    {
        Debug.Log($"Starting Wave {waveNumber}!");

        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.SetSpawning(true);
                totalEnemies += spawner.spawnCount;
            }
        }

        waveNumber++;
    }

    public void OnEnemyKilled(Enemy enemy)
    {
        totalEnemies--;
        activeEnemies.Remove(enemy); // Remove the killed enemy from the active list

        if (totalEnemies <= 0)
        {
            Debug.Log("All enemies defeated. Prepare for the next wave!");
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        activeEnemies.Add(enemy); // Add the spawned enemy to the active list
    }
}
