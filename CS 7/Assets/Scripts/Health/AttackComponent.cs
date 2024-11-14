using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Ensure a Collider2D component is attached
public class AttackComponent : MonoBehaviour
{
    [Header("Attack Settings")]
    public Bullet bullet; // Reference to a bullet or projectile (optional)
    public int damage = 10; // Amount of damage dealt on collision

    private void Awake()
    {
        // Ensure the collider is set as a trigger
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null && !collider.isTrigger)
        {
            collider.isTrigger = true; // Set collider as a trigger
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Step 5a: Check if the colliding object has the same tag
        if (other.CompareTag(gameObject.tag))
        {
            return; // Exit if the colliding object has the same tag
        }

        // Step 5b: Check if the colliding object has a HitboxComponent
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            // Call the Damage method with the specified damage amount
            hitbox.Damage(damage);
        }

        InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();
        if (invincibility != null)
        {
            invincibility.TriggerInvincibility();
        }
        
    }
}
