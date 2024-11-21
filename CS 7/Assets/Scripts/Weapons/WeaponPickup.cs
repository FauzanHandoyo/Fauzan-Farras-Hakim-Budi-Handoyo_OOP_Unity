using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder; // Reference to the weapon associated with this pickup
    private Weapon weapon;

    void Awake()
    {
        // Initialize the weapon from the holder
        weapon = weaponHolder;
    }

    void Start()
    {
        // Disable weapon visuals initially
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Equip this weapon
            EquipWeapon(other);

            // Hide all other weapons in the scene
            WeaponManager.HideAllWeapons();
        }
    }

    private void EquipWeapon(Collider2D player)
    {
        // Attach the weapon to the player
        weapon.transform.SetParent(player.transform);

        // Position the weapon correctly relative to the player
        weapon.transform.localPosition = Vector3.zero;

        // Enable the weapon visuals
        TurnVisual(true);
    }

    public void DisableWeaponPickup()
    {
        // Disable the weapon pickup GameObject
        gameObject.SetActive(false);
    }

    public void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
}
