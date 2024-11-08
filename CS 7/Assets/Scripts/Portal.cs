using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed;            // Movement speed of the portal
    [SerializeField] private float rotateSpeed;      // Rotation speed of the portal

     Vector2 newPosition;                     // Target position for the portal to move towards
     LevelManager levelManager;               // Reference to the LevelManager for loading scenes

    private void Start()
    {
        // Find the LevelManager in the scene
        levelManager = FindObjectOfType<LevelManager>();

        // Initialize the newPosition with a random position
        ChangePosition();
    }

    private void Update()
    {
        // Move the portal towards the target position (newPosition)
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Rotate the portal
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // Check if the portal is close to the target position (within 0.5 units)
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition(); // Generate a new random position
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Weapon weapon = player.GetComponentInChildren<Weapon>();
            bool hasWeapon = weapon != null;
            
            GetComponent<SpriteRenderer>().enabled = hasWeapon;
            GetComponent<Collider2D>().enabled = hasWeapon;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the portal collides with the player
        if (other.CompareTag("Player"))
        {
            // Load the "Main" scene using the LevelManager
            if (levelManager != null)
            {
                levelManager.LoadScene("Main");
            }
        }
    }

    // Set a new random target position for the portal to move towards
    private void ChangePosition()
    {
        // Generate a random position within a certain range (adjust as needed)
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-10f, 10f);
        newPosition = new Vector2(randomX, randomY);
    }
}
