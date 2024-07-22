using Godot;

public partial class Player : CharacterBody3D
{
    /// <summary>
    /// Acceleration in (m/s^2) for sideways acceleration as result of player input or lack thereof.
    /// </summary>
    public float Acceleration { get; set; } = 20f;

    /// <summary>
    /// Max sideways speed.
    /// </summary>
    public float MaxSpeed { get; set; } = 4f;

    /// <summary>
    /// Acceleration along the z-axis as a result of gravity.
    /// </summary>
    public float Gravity { get; set; } = -10f;

    /// <summary>
    /// A multiplier to the willful sideways acceleration as a result of not touching the floor.
    /// </summary>
    public float OffFloorWillfulAccelerationMultiplier { get; set; } = 0.5f;

    public override void _Ready() { }

    public override void _PhysicsProcess(double delta)
    {
        ProcessGravity(delta);
        ProcessSidewaysPlayerMovement(delta);
        MoveAndSlide();
    }

    private void ProcessGravity(double delta)
    {
        Velocity += new Vector3(0, (float)(Gravity * delta), 0);
    }

    private void ProcessSidewaysPlayerMovement(double delta)
    {
        float willfulAccelerationMultiplier = IsOnFloor()
            ? 1f
            : OffFloorWillfulAccelerationMultiplier;

        float newX = Velocity.X;
        if (Input.IsActionPressed("level__left"))
        {
            newX = Mathf.Max(
                -MaxSpeed,
                (float)(newX - (Acceleration * delta * willfulAccelerationMultiplier))
            );
        }
        else if (Input.IsActionPressed("level__right"))
        {
            newX = Mathf.Min(
                MaxSpeed,
                (float)(newX + (Acceleration * delta * willfulAccelerationMultiplier))
            );
        }
        else if (newX != 0 && IsOnFloor())
        {
            newX =
                Mathf.Sign(newX) * Mathf.Max(0, (float)(Mathf.Abs(newX) - (Acceleration * delta)));
        }

        Velocity = new Vector3(newX, Velocity.Y, Velocity.Z);
    }
}
