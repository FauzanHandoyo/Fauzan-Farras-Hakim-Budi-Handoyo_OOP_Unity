using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [Header("Invincibility Settings")]
    [SerializeField] private int blinkingCount = 7;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;

    private void Awake()
    {
        // Get the SpriteRenderer and store the original material
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    // Enumerator for blinking effect
    private IEnumerator Blink()
    {
        isInvincible = true;

        for (int i = 0; i < blinkingCount; i++)
        {
            spriteRenderer.material = blinkMaterial; // Switch to blink material
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.material = originalMaterial; // Switch back to original material
            yield return new WaitForSeconds(blinkInterval);
        }

        isInvincible = false;
    }

    // Method to trigger invincibility
    public void TriggerInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(Blink());
        }
    }
}
