using System;
using UnityEngine;

[RequireComponent(typeof(GroundDetection))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSidewaysMovement : MonoBehaviour
{
  private const float ACCELERATION = 20f;
  private const float MAX_SPEED = 5f;
  private const float OFF_GROUND_MODIFIER = 0.5f;

  private GroundDetection groundDetection;
  private Rigidbody2D rigidbody2d;

  private void FixedUpdate()
  {
    ProcessSidewaysAcceleration();
    ProcessComingToRest();
  }

  private void ProcessComingToRest()
  {
    bool isMovingAndAcceleratingToTheLeft = Input.GetKey(KeyCode.A) && rigidbody2d.velocity.x < 0;
    bool isMovingAndAcceleratingToTheRight = Input.GetKey(KeyCode.D) && 0 < rigidbody2d.velocity.x;
    bool isMovingAndAcceleratingInSameDirection = isMovingAndAcceleratingToTheLeft || isMovingAndAcceleratingToTheRight;

    if (isMovingAndAcceleratingInSameDirection)
      return;
    // The player is not accelerating in the same direction as they are moving.

    if (!groundDetection.IsGrounded)
      return;
    // The player is on the ground.

    // The player should come to rest.

    float newXVelocity =
      MathF.Sign(rigidbody2d.velocity.x)
      * MathF.Max(0, MathF.Abs(rigidbody2d.velocity.x) - ACCELERATION * Time.deltaTime);
    rigidbody2d.velocity = new Vector2(newXVelocity, rigidbody2d.velocity.y);
  }

  private void ProcessSidewaysAcceleration()
  {
    float sidewaysAccelerationDirection = 0f;
    if (Input.GetKey(KeyCode.A))
    {
      sidewaysAccelerationDirection -= 1f;
    }
    if (Input.GetKey(KeyCode.D))
    {
      sidewaysAccelerationDirection += 1f;
    }

    float sidewaysAccelerationModifier = 1f;
    sidewaysAccelerationModifier *= groundDetection.IsGrounded ? 1f : OFF_GROUND_MODIFIER;

    rigidbody2d.velocity = new Vector2(
      Mathf.Clamp(
        rigidbody2d.velocity.x
          + sidewaysAccelerationDirection * sidewaysAccelerationModifier * ACCELERATION * Time.deltaTime,
        -MAX_SPEED,
        MAX_SPEED
      ),
      rigidbody2d.velocity.y
    );
  }

  private void Start()
  {
    groundDetection = GetComponent<GroundDetection>();
    rigidbody2d = GetComponent<Rigidbody2D>();
  }
}
