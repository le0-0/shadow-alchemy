using UnityEngine;

public class GroundDetection : MonoBehaviour
{
  private const float CHECK_LENGTH = 0.05f;

  [SerializeField]
  private LayerMask groundLayerMask;

  [SerializeField]
  private Vector2 contactBoxCenterOffset;

  [SerializeField]
  private Vector2 contactBoxSize;

  private bool cache = false;
  private float cacheTime = 0;

  private Vector3 ContactBoxCenter
  {
    get { return transform.position + (Vector3)contactBoxCenterOffset; }
  }

  public bool IsGrounded
  {
    get
    {
      if (cacheTime == Time.time)
        return cache;

      RaycastHit2D boxCastResult = Physics2D.BoxCast(
        ContactBoxCenter,
        contactBoxSize,
        0,
        Vector2.down,
        CHECK_LENGTH,
        groundLayerMask
      );

      cache = boxCastResult.collider != null;
      cacheTime = Time.time;

      return cache;
    }
  }

  void OnDrawGizmosSelected()
  {
    Vector3 contactSurfaceCenter = ContactBoxCenter;

    Gizmos.color = Color.green;
    Gizmos.DrawWireCube(contactSurfaceCenter, contactBoxSize);

    Gizmos.color = Color.blue;

    Vector3 leftStart = contactSurfaceCenter + new Vector3(-0.5f * contactBoxSize.x, -0.5f * contactBoxSize.y, 0);
    Vector3 leftEnd = leftStart + new Vector3(0, -CHECK_LENGTH, 0);
    GizmosUtility.DrawArrow2D(leftStart, leftEnd);

    Vector3 rightStart = contactSurfaceCenter + new Vector3(0.5f * contactBoxSize.x, -0.5f * contactBoxSize.y, 0);
    Vector3 rightEnd = rightStart + new Vector3(0, -CHECK_LENGTH, 0);
    GizmosUtility.DrawArrow2D(rightStart, rightEnd);

    Gizmos.DrawLine(leftEnd, rightEnd);
  }
}
