using UnityEngine;

public class EnemyForward : Enemy
{
    private Vector2 moveDirection = Vector2.up; // Always move downward
    private float screenHalfHeight;

    private void Start()
    {
        SetSpawnPosition();
        
        // Calculate the half-height of the screen in world units
        screenHalfHeight = Camera.main.orthographicSize;
    }

    private void Update()
    {
        // Move the enemy downward
        transform.Translate(moveDirection * Time.deltaTime);

        // Check if the enemy goes off the bottom of the screen and destroy it if necessary
        if (transform.position.y < -screenHalfHeight - 1) // Add buffer to detect off-screen exit
        {
            Destroy(gameObject); // Or return to a pool if using object pooling
        }
    }

    private void SetSpawnPosition()
    {
        // Spawn randomly along the top of the screen
        float spawnX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
        float spawnY = screenHalfHeight + 1; // Start just off the top of the screen
        transform.position = new Vector2(spawnX, spawnY);
    }
}
