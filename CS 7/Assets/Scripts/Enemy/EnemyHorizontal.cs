using UnityEngine;

public class EnemyHorizontal : Enemy
{
    protected Vector2 moveDirection; // Change to protected
    protected float screenHalfWidth; // Change to protected

    private void Start()
    {
        SetRandomSpawnPosition();
        SetMoveDirection();
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void Update()
    {
        transform.Translate(moveDirection * Time.deltaTime);

        if (Mathf.Abs(transform.position.x) > screenHalfWidth + 1)
        {
            moveDirection.x = -moveDirection.x; // Reverse direction
        }
    }

    protected void SetRandomSpawnPosition()
    {
        float spawnX = Random.Range(0, 2) == 0 ? -screenHalfWidth - 1 : screenHalfWidth + 1;
        float spawnY = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        transform.position = new Vector2(spawnX, spawnY);
    }

    protected void SetMoveDirection()
    {
        if (transform.position.x < 0)
        {
            moveDirection = Vector2.right;
        }
        else
        {
            moveDirection = Vector2.left;
        }
    }
}
