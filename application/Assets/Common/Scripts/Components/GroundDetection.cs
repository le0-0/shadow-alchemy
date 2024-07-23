using UnityEngine;

public class GroundDetection : MonoBehaviour
{
  [SerializeField]
  private LayerMask groundLayerMask;

  [SerializeField]
  private Vector2 contactSurfaceCenterOffset;

  [SerializeField]
  private float contactSurfaceWidth;

  [SerializeField]
  private float contactProtrusion;

  void Start() { }

  void Update() { }
}
