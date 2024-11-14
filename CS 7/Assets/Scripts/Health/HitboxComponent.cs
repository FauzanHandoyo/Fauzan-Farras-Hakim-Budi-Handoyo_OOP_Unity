using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Ensures a Collider2D component is attached
public class HitboxComponent : MonoBehaviour
{
    private HealthComponent healthComponent;

    private void Awake()
    {
        // Get the HealthComponent on this GameObject
        healthComponent = GetComponent<HealthComponent>();

        // If no HealthComponent is found, log a warning (optional)
        if (healthComponent == null)
        {
            Debug.LogWarning("HealthComponent is missing on " + gameObject.name);
        }
    }

    // Overloaded Damage method that takes an integer value
    public void Damage(int amount)
    {
        if (healthComponent != null)
        {
            healthComponent.Subtract(amount);
        }
    }

    // Overloaded Damage method that takes a Bullet instance
    public void Damage(Bullet bullet)
    {
        if (healthComponent != null)
        {
            healthComponent.Subtract(bullet.damage); // Assume Bullet has a 'damage' property
        }
    }
}
