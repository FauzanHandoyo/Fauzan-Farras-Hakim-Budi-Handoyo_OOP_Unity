using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } // Singleton instance

    private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake()
    {
        // Singleton pattern: Ensure only one instance of Player exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }
        
        Instance = this; // Set the instance
        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {
        // init PlayerMovement and Animator components
        playerMovement = GetComponent<PlayerMovement>();
        animator = GameObject.Find("EngineEffect").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
       //buat movement player
        playerMovement.Move();
    }

    void LateUpdate()
    {
        //is moving animation
        animator.SetBool("isMoving", playerMovement.IsMoving());
    }
}
