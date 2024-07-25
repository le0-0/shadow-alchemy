using UnityEngine;

internal static partial class RandomUtility
{
  public static Vector2 Vector2(float minRadius = 0, float maxRadius = 1)
  {
    float x;
    float y;
    float squareRadius;
    do
    {
      x = Random.Range(-maxRadius, maxRadius);
      y = Random.Range(-maxRadius, maxRadius);
      squareRadius = x * x + y * y;
    } while (maxRadius * maxRadius < squareRadius || squareRadius < minRadius * minRadius);

    return new Vector2(x, y);
  }
}
