using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    void Awake()
    {
        // Initialize weapon with the weaponHolder object
        weapon = weaponHolder;
    }

    void Start()
    {
        // If weapon is not null, initialize related methods with false
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the Player
        if (other.CompareTag("Player"))
        {
            // Set the parent of the weapon to the Player
            weapon.transform.SetParent(other.transform);

            // Set the local position to place it correctly relative to the player
            weapon.transform.localPosition = Vector3.zero; // or a specific offset like new Vector3(1, 0, 0)

            // Enable the visual components of the weapon
            TurnVisual(true, weapon);
        }
    }


    // Method overloading for TurnVisual
    public void TurnVisual(bool on)
    {
        // Enable or disable weapon components based on 'on' parameter
        weapon.gameObject.SetActive(on);
    }

    public void TurnVisual(bool on, Weapon weapon)
    {
        // Enable or disable weapon components based on 'on' parameter
        weapon.gameObject.SetActive(on);
    }
}
