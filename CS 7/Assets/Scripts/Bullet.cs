using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> pool; // Field to store the pool reference

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetPool(IObjectPool<Bullet> objectPool)
    {
        pool = objectPool; // Assign the pool reference when called from Weapon
    }

    private void OnEnable()
    {
        // Set bullet velocity in the forward direction of bulletSpawnPoint
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has a HitboxComponent
        HitboxComponent hitbox = collision.GetComponent<HitboxComponent>();

        if (hitbox != null)
        {
            // Deal damage to the object
            hitbox.Damage(damage);

            // Return the bullet to the pool after dealing damage
            ReturnToPool();
        }
    }

    private void OnBecameInvisible()
    {
        // Return bullet to the pool when it leaves the camera view
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        // Use the pool to release this bullet instance, returning it to the pool
        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            gameObject.SetActive(false); // If no pool is assigned, just deactivate it
        }
    }
}
