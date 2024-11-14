using UnityEngine;

public class EnemyBoss : EnemyHorizontal
{
    [Header("Boss Weapon Settings")]
    [SerializeField] Weapon bossWeapon; 
    [SerializeField] private float shootInterval = 2f; 

    private float shootTimer;

    private void Start()
    {
        SetRandomSpawnPosition();
        SetMoveDirection();
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        shootTimer = shootInterval;
    }

    private void Update()
    {
        transform.Translate(moveDirection * Time.deltaTime);

        if (Mathf.Abs(transform.position.x) > screenHalfWidth + 1)
        {
            moveDirection.x = -moveDirection.x;
        }

        HandleShooting();
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            bossWeapon.Shoot(); 
            shootTimer = shootInterval;
        }
    }
}
