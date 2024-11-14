using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Maximum health of the entity
    private int currentHealth;

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

        // Check if health falls below or equals zero
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Optional: clamp health to 0
            OnDeath();
        }
    }

    // Method to handle the object's destruction
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
