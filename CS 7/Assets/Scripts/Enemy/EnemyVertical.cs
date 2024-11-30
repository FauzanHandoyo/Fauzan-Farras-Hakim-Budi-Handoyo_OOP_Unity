using UnityEngine;

public class EnemyForward : Enemy
{
    private Vector2 moveDirection = Vector2.up; // Default direction is downward
    private float screenHalfWidth; // Half the width of the screen in world units
    private float screenHalfHeight; // Half the height of the screen in world units
    private Vector2 velocity = new Vector2(1f, -1f); // Horizontal and vertical speed

    private void Start()
    {
        SetSpawnPosition();

        // Calculate the half-width and half-height of the screen in world units
        screenHalfHeight = Camera.main.orthographicSize;
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void Update()
    {
        // Move the enemy based on velocity
        transform.Translate(velocity * Time.deltaTime);

        // Bounce horizontally if the enemy hits the screen edges
        if (transform.position.x < -screenHalfWidth || transform.position.x > screenHalfWidth)
        {
            velocity.x *= -1; // Reverse horizontal direction
        }

        // Destroy the enemy if it moves off the bottom of the screen
        if (transform.position.y < -screenHalfHeight - 1)
        {
            Destroy(gameObject); // Or return to a pool if using object pooling
        }
    }

    private void SetSpawnPosition()
    {
        // Spawn randomly along the top of the screen
        float spawnX = Random.Range(-screenHalfWidth, screenHalfWidth);
        float spawnY = screenHalfHeight + 1; // Start just off the top of the screen
        transform.position = new Vector2(spawnX, spawnY);
    }
}
