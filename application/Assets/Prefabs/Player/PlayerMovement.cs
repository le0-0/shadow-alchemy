using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
  private const float ACCELERATION = 20f;
  private const float MAX_SPEED = 5f;
  private const float OFF_GROUND_MODIFIER = 0.5f;

  private GroundDetection groundDetection;
  private Rigidbody2D rigidbody2d;

  private void FixedUpdate()
  {
    bool isAcceleratingSameDirectionAsMovement = false;
    float sidewaysAccelerationModifier = 1f;
    sidewaysAccelerationModifier *= groundDetection.IsGrounded ? 1f : OFF_GROUND_MODIFIER;
    float xVelocity = rigidbody2d.velocity.x;

    if (Input.GetKey(KeyCode.A))
    {
      xVelocity = MathF.Max(-MAX_SPEED, xVelocity - sidewaysAccelerationModifier * ACCELERATION * Time.deltaTime);
      isAcceleratingSameDirectionAsMovement = xVelocity < 0;
    }
    if (Input.GetKey(KeyCode.D))
    {
      xVelocity = MathF.Min(MAX_SPEED, xVelocity + sidewaysAccelerationModifier * ACCELERATION * Time.deltaTime);
      isAcceleratingSameDirectionAsMovement = 0 < xVelocity;
    }

    if (!isAcceleratingSameDirectionAsMovement && groundDetection.IsGrounded)
    {
      xVelocity = MathF.Sign(xVelocity) * MathF.Max(0, MathF.Abs(xVelocity) - ACCELERATION * Time.deltaTime);
    }

    rigidbody2d.velocity = new Vector2(xVelocity, rigidbody2d.velocity.y);
  }

  private void Start()
  {
    groundDetection = GetComponent<GroundDetection>();
    rigidbody2d = GetComponent<Rigidbody2D>();
  }
}
