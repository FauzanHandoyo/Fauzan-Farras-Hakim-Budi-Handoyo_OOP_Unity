using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 1; // The level of the enemy, used to calculate points
    public int points; // Points assigned to this enemy

    public delegate void EnemyKilled(int points); // Delegate for enemy killed event
    public event EnemyKilled OnEnemyKilled; // Event triggered when the enemy dies

    private HealthComponent healthComponent; // Reference to health component

    private void Start()
    {
        healthComponent = GetComponent<HealthComponent>(); // Get the health component attached to the enemy
        points = level * 2; // Example: Points are calculated based on the enemy level (Level * 2)
    }

    public void TakeDamage(int damage)
    {
        healthComponent.Subtract(damage); // Call the health component to subtract health

        if (healthComponent.GetHealth() <= 0)
        {
            // If the enemy is dead, trigger the enemy killed event
            OnEnemyDeath();
        }
    }

    private void OnEnemyDeath()
    {
        // Trigger the event and notify any listeners (like PointsManager)
        OnEnemyKilled?.Invoke(points);

        // Destroy the enemy after death
        Destroy(gameObject);
    }
}
