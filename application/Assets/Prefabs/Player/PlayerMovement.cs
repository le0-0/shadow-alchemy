using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
  private const float ACCELERATION = 20f;
  private const float MAX_SPEED = 5f;
  private const float OFF_GROUND_MODIFIER = 0.5f;

  private Rigidbody2D rigidbody2d;

  void Start()
  {
    rigidbody2d = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate()
  {
    bool isAcceleratingSideways = false;
    float xVelocity = rigidbody2d.velocity.x;

    if (Input.GetKey(KeyCode.A))
    {
      isAcceleratingSideways = true;
      xVelocity = MathF.Max(-MAX_SPEED, xVelocity - (ACCELERATION * Time.deltaTime));
    }
    if (Input.GetKey(KeyCode.D))
    {
      isAcceleratingSideways = true;
      xVelocity = MathF.Min(MAX_SPEED, xVelocity + (ACCELERATION * Time.deltaTime));
    }

    if (!isAcceleratingSideways)
    {
      xVelocity = MathF.Sign(xVelocity) * MathF.Max(0, MathF.Abs(xVelocity) - (ACCELERATION * Time.deltaTime));
    }

    rigidbody2d.velocity = new Vector2(xVelocity, rigidbody2d.velocity.y);
  }
}
