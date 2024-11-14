using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    
    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    
    private float timer;
    public Transform parentTransform;

    private void Awake()
    {
        // Initialize the object pool for bullets
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            collectionCheck, 
            defaultCapacity, 
            maxSize);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0;
        }
    }

    private Bullet CreateBullet()
    {
        // Instantiate a new bullet and set it inactive initially
        Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBullet.gameObject.SetActive(false);
        newBullet.SetPool(objectPool); // Optional: if bullet needs access to the pool
        return newBullet;
    }

    private void OnTakeFromPool(Bullet bullet)
    {
        // Activate and set the bullet's position and rotation when taken from the pool
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Bullet bullet)
    {
        // Deactivate the bullet when it's returned to the pool
        bullet.gameObject.SetActive(false);
    }
    
    private void OnDestroyPoolObject(Bullet bullet)
    {
        // Destroy the bullet object when removed from the pool permanently
        Destroy(bullet.gameObject);
    }
    
    public void Shoot()
    {
        // Take a bullet from the pool and shoot it
         Bullet bullet = objectPool.Get();

        // Set the bullet's position and rotation to match the bulletSpawnPoint
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;

        // Set the bullet's velocity
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = bulletSpawnPoint.up * bullet.bulletSpeed; // Use bulletSpawnPoint's forward direction
        }
    }
    
}

