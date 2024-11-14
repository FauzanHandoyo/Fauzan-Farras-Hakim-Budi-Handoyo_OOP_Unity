using UnityEngine;
using UnityEngine.Assertions;

public class EnemyClickSpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyVariants;
    [SerializeField] private int selectedVariant = 0;

    // Start is called before the first frame update
    private void Start()
    {
        Assert.IsTrue(enemyVariants.Length > 0, "Tambahkan setidaknya 1 Prefab Enemy terlebih dahulu!");
    }

    private void Update()
    {
        // Check number keys to select enemy variant
        for (int i = 1; i <= enemyVariants.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                selectedVariant = i - 1;
            }
        }

        // Right mouse click to spawn selected enemy variant
        if (Input.GetMouseButtonDown(1))
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (selectedVariant < enemyVariants.Length)
        {
            Instantiate(enemyVariants[selectedVariant]);
        }
    }
}
