using TMPro;
using UnityEngine;
using UnityEngine.UI; // For UI components

public class MainGameUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI healthText; // Reference to display player health
    [SerializeField] private TextMeshProUGUI pointsText; // Reference to display points

    private HealthComponent playerHealthComponent; // Reference to the player's health component
    private int totalPoints = 0; // Keep track of the player's points

    // Start is called before the first frame update
    void Start()
    {
        // Find the player's HealthComponent (assumes Player has this component)
        playerHealthComponent = FindObjectOfType<PlayerHealth>().GetComponent<HealthComponent>();

        // Ensure the playerHealthComponent exists
        if (playerHealthComponent != null)
        {
            // Subscribe to the health change event
            playerHealthComponent.OnHealthChanged += UpdateHealthUI;

            // Initialize the health UI
            UpdateHealthUI(playerHealthComponent.GetHealth(), playerHealthComponent.GetHealth());
        }

        else
        {
            Debug.LogError("PlayerHealth or HealthComponent not found!");
        }
    }

    // Method to update the health display in the UI
    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        // Update the health text UI element to reflect the player's current health
        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }

    // Method to update points (e.g., when an enemy is killed)
    public void UpdatePoints(int points)
    {
        totalPoints += points;
        pointsText.text = "Points: " + totalPoints;
    }

    // Optional: Update health directly in the UI (called by some external script)
    public void SetPlayerHealth(int health)
    {
        playerHealthComponent.Subtract(health); // Use this to decrease health manually
    }
}
