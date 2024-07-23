using UnityEngine;

[RequireComponent(typeof(GroundDetection))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumping : MonoBehaviour
{
  private const float JUMP_COOLDOWN = 0.1f;
  private const float VERTICAL_SPEED_OF_JUMP = 6.5f;

  private GroundDetection groundDetection;
  private Rigidbody2D rigidbody2d;

  private float timeSinceLastJump = JUMP_COOLDOWN;

  private void Start()
  {
    groundDetection = GetComponent<GroundDetection>();
    rigidbody2d = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    timeSinceLastJump += Time.deltaTime;

    if (timeSinceLastJump < JUMP_COOLDOWN)
      return;

    if (!Input.GetKeyDown(KeyCode.W))
      return;

    rigidbody2d.velocity += VERTICAL_SPEED_OF_JUMP * Vector2.up;
    timeSinceLastJump = 0;
  }
}
