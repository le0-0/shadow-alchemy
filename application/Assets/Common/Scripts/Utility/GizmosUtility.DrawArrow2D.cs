using UnityEngine;

internal static partial class GizmosUtility
{
  private const float ARROW_BARB_LENGTH = 0.1f;
  private const float ARROW_INNER_ANGLE = 30f;

  private static readonly Matrix4x4 rotateInnerAngleClockwise = Matrix4x4.Rotate(
    Quaternion.Euler(new Vector3(0, 0, -ARROW_INNER_ANGLE))
  );
  private static readonly Matrix4x4 rotateInnerAngleCounterclockwise = Matrix4x4.Rotate(
    Quaternion.Euler(new Vector3(0, 0, ARROW_INNER_ANGLE))
  );

  public static void DrawArrow2D(Vector2 from, Vector2 to)
  {
    Gizmos.DrawLine(from, to);

    Vector2 reverseMainLine = from - to;
    Gizmos.DrawLine(
      to,
      to + ARROW_BARB_LENGTH * (Vector2)rotateInnerAngleClockwise.MultiplyVector(reverseMainLine).normalized
    );
    Gizmos.DrawLine(
      to,
      to + ARROW_BARB_LENGTH * (Vector2)rotateInnerAngleCounterclockwise.MultiplyVector(reverseMainLine).normalized
    );
  }
}
