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

    private GameObject player;         // Reference to the player GameObject
    private bool playerHasWeapon = false; // Flag to check if the player has a weapon

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        CheckPlayerWeapon();
    }

    private void Update()
    {
        // Check if the player has acquired a weapon
        CheckPlayerWeapon();

        // Only proceed with spawning logic if the player has a weapon
        if (playerHasWeapon)
        {
            timer += Time.deltaTime;

            if (timer >= waveInterval && totalEnemies <= 0)
            {
                StartWave();
                timer = 0f;
            }
        }
    }

    private void StartWave()
    {
        if (!playerHasWeapon)
        {
            Debug.Log("Player does not have a weapon. Cannot start the wave.");
            return;
        }

        Debug.Log($"Starting Wave {waveNumber}!");

        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.SetSpawning(true); // Enable spawning for each spawner
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

    private void CheckPlayerWeapon()
    {
        if (player != null)
        {
            Weapon weapon = player.GetComponentInChildren<Weapon>();
            playerHasWeapon = weapon != null;

            // Enable/Disable enemy spawners based on weapon status
            foreach (EnemySpawner spawner in enemySpawners)
            {
                if (spawner != null)
                {
                    spawner.SetSpawning(playerHasWeapon);
                }
            }
        }
    }
}
