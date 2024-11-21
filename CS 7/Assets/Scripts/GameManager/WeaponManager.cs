using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private static WeaponManager instance; // Singleton instance for global management
    private WeaponPickup[] allWeapons; // All weapons in the scene

    void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Find all weapon pickups in the scene
        allWeapons = FindObjectsOfType<WeaponPickup>();
    }

    public static void HideAllWeapons()
    {
        if (instance == null) return;

        // Loop through all weapons and disable them
        foreach (var weapon in instance.allWeapons)
        {
            weapon.DisableWeaponPickup();
        }
    }
}
