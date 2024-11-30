using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private HealthComponent healthComponent;

    // Start is called before the first frame update
    void Start()
    {
        healthComponent = GetComponent<HealthComponent>(); // Assuming Player has a HealthComponent attached
    }

    // Method to reduce health when an enemy is hit
    public void TakeDamage(int damage)
    {
        healthComponent.Subtract(damage); // Call healthComponent's Subtract method to reduce health
    }
}

