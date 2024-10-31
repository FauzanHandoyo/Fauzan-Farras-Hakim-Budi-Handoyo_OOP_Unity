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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // values cs
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // input user
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // jika ada input maka ke tempat atau ke koordinat tersebut
        if (moveDirection != Vector2.zero)
        {
            // Smoothly accelerate using moveVelocity and apply movement direction
            Vector2 targetVelocity = moveDirection * maxSpeed;
            Vector2 velocityChange = moveDirection * moveVelocity * Time.fixedDeltaTime; //biar smooth

            // clamp buat jika spaceship tidak melebihi kecepatan
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + velocityChange, maxSpeed.magnitude);
        }
        else
        {
            // gunakan friction ketika tidak ada input
            Vector2 frictionForce = GetFriction() * Time.fixedDeltaTime;
            rb.velocity += frictionForce;

            // ngeclamp ke 0 jika tidak ada input
            if (rb.velocity.magnitude < stopClamp.magnitude)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private Vector2 GetFriction()
    {
        // Check if the velocity is greater than zero
        if (rb.velocity.sqrMagnitude > 0)
        {
            // Return movement friction
            return moveFriction * rb.velocity.normalized;
        }
        else
        {
            // Return stopping friction
            return stopFriction;
        }
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f;
    }
}
