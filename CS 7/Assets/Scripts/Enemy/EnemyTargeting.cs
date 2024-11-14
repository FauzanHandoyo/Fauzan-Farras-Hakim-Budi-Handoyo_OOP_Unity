using UnityEngine;

public class EnemyTargeting : Enemy
{
    [Header("Targeting Settings")]
    [SerializeField] private float speed = 3f; // Speed at which the enemy moves towards the player
    private Transform playerTransform;

    private void Start()
    {
        // Find the player in the scene by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        // Check if the player object is found and retrieve its transform
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        // Only move towards the player if a reference is available
        if (playerTransform != null)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Move the enemy towards the player at the defined speed
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

        // Optionally rotate the enemy to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
