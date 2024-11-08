using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;

    private float xMin, xMax, yMin, yMax;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Calculate move velocity and friction
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);

        // Set screen boundaries
        Camera cam = Camera.main;
        float distance = transform.position.z - cam.transform.position.z;

        float paddingMax = 0.5f;

        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + paddingMax;
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - paddingMax;
        yMin = cam.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + paddingMax;
        yMax = cam.ViewportToWorldPoint(new Vector3(0, 1, distance)).y - paddingMax;
    }

    private void FixedUpdate()
    {
        Move();

        // Clamp the spaceship's position within the screen bounds
        float clampedX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float clampedY = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector2(clampedX, clampedY);
    }

    public void Move()
    {
        // User input
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (moveDirection != Vector2.zero)
        {
            Vector2 targetVelocity = moveDirection * maxSpeed;
            Vector2 velocityChange = moveDirection * moveVelocity * Time.fixedDeltaTime;

            // Apply velocity change and clamp the speed
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + velocityChange, maxSpeed.magnitude);
        }
        else
        {
            Vector2 frictionForce = GetFriction() * Time.fixedDeltaTime;
            rb.velocity += frictionForce;

            if (rb.velocity.magnitude < stopClamp.magnitude)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private Vector2 GetFriction()
    {
        if (rb.velocity.sqrMagnitude > 0)
        {
            return moveFriction * rb.velocity.normalized;
        }
        else
        {
            return stopFriction;
        }
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f;
    }
}
