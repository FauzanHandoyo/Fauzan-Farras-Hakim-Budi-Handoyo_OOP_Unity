using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Maximum health of the entity
    private int currentHealth;

    // Event that gets called when health changes
    public event System.Action<int, int> OnHealthChanged;

    private void Awake()
    {
        // Set the current health to maxHealth at the start
        currentHealth = maxHealth;
    }

    // Getter for current health
    public int GetHealth()
    {
        return currentHealth;
    }

    // Method to reduce health by a certain amount
    public void Subtract(int amount)
    {
        currentHealth -= amount;

        // Ensure health doesn't go below zero
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Trigger the health changed event
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        // Check if health falls below or equals zero (death logic here)
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        // Optionally add effects, animations, or callbacks here before destruction
        Destroy(gameObject); // Destroy the GameObject this component is attached to
    }

    // Optional method to reset or set max health dynamically
    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth; // Reset current health to new max health
    }
}
