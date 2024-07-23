using UnityEngine;

internal static partial class GizmosUtility
{
  private const float POINT_RADIUS = 0.05f;

  public static void DrawPoint(Vector3 center)
  {
    Gizmos.DrawSphere(center, POINT_RADIUS);
  }
}
