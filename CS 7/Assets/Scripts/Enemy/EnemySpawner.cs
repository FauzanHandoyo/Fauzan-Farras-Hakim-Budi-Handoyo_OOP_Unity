using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy; // Prefab of the enemy to spawn

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3; // Kills required to increase spawn count
    public int totalKill = 0; // Total number of enemies killed by the player
    private int totalKillWave = 0; // Kills within the current wave

    [SerializeField] private float spawnInterval = 3f; // Interval between enemy spawns

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0; // Current number of enemies to spawn per wave
    public int defaultSpawnCount = 1; // Initial spawn count per wave
    public int spawnCountMultiplier = 1; // Multiplier for spawn count
    public int multiplierIncreaseCount = 1; // Amount to increase the multiplier by

    public CombatManager combatManager; // Reference to a CombatManager for game logic

    public bool isSpawning = false; // Whether the spawner is currently active

    private void Start()
    {
        // Initialize the spawn count with the default value
        spawnCount = defaultSpawnCount;

        // Start the spawning coroutine
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // Continuously spawn enemies while `isSpawning` is true
        while (true)
        {
            if (isSpawning && spawnedEnemy != null)
            {
                // Spawn the current number of enemies
                for (int i = 0; i < spawnCount; i++)
                {
                    SpawnEnemy();
                    yield return new WaitForSeconds(0.5f); // Slight delay between individual spawns
                }

                // Wait for the defined spawn interval before the next wave
                yield return new WaitForSeconds(spawnInterval);

                // Check if the spawn count needs to be increased based on kills
                CheckAndIncreaseSpawnCount();
            }
            else
            {
                yield return null; // Wait until spawning is enabled
            }
        }
    }

    private void SpawnEnemy()
    {
        // Instantiate an enemy at the spawner's position
        Enemy newEnemy = Instantiate(spawnedEnemy, transform.position, Quaternion.identity);

        // Optionally, set up enemy-specific properties
        if (combatManager != null)
        {
            combatManager.RegisterEnemy(newEnemy);
        }
    }

    private void CheckAndIncreaseSpawnCount()
    {
        // Check if the total kills meet the threshold to increase spawn count
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            // Increment the spawn count multiplier and update the spawn count
            spawnCountMultiplier += multiplierIncreaseCount;
            spawnCount = defaultSpawnCount * spawnCountMultiplier;

            // Reset the total kills for the current wave
            totalKillWave = 0;
        }
    }

    // Method to be called by enemies when they are defeated
    public void OnEnemyKilled()
    {
        totalKill++; // Increment the total kills
        totalKillWave++; // Increment the kills for the current wave
    }

    // Method to start or stop spawning
    public void SetSpawning(bool isActive)
    {
        isSpawning = isActive;
    }
}
