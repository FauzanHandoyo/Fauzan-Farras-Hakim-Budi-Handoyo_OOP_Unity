using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Button RestartButton; // Reference to the restart button

    // Start is called before the first frame update
    void Start()
    {
        // Check if the button is assigned, then add listener for the restart button click
        if (RestartButton != null)
        {
            RestartButton.onClick.AddListener(OnRestartButtonClicked);
        }
    }

    // Method called when the Restart button is clicked
    void OnRestartButtonClicked()
    {
        // Find the player object (assuming it has the tag "Player")
        GameObject player = GameObject.FindWithTag("Player");

        // If the player object is found, destroy it
        if (player != null)
        {
            Destroy(player);
        }

        // Load the "ChooseWeapon" scene to reset the game
        SceneManager.LoadScene("ChooseWeapon");
    }
}
