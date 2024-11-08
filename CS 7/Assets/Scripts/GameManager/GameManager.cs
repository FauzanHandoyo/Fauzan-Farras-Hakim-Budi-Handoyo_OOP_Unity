using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This makes GameManager and its children persistent

            // Initialize the LevelManager (assuming it's a child of GameManager)
            LevelManager = GetComponentInChildren<LevelManager>();

            DontDestroyOnLoad(GameObject.Find("Main Camera"));
        }
    }

    // Method to remove all objects in the scene except the Camera and Player
    public void ClearScene()
    {
        // Get all root GameObjects in the scene
        var allObjects = FindObjectsOfType<GameObject>().Where(obj => obj.transform.parent == null);

        foreach (var obj in allObjects)
        {
            // Keep only objects tagged as "MainCamera" or "Player"
            if (obj.CompareTag("MainCamera") || obj.CompareTag("Player"))
            {
                continue;
            }

            // Destroy all other objects
            Destroy(obj);
        }
    }
}
