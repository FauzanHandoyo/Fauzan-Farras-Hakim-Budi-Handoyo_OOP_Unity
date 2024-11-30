using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager to change scenes
using UnityEngine.UI; // Import for UI components like Button

public class MainMenu : MonoBehaviour
{
    // Reference to the Button UI element
    public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the Button is set in the inspector, then add the listener for the button click
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
    }

    // Called when the start button is clicked
    void OnStartButtonClicked()
    {
        // Load the "ChooseWeapon" scene
        SceneManager.LoadScene("ChooseWeapon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
