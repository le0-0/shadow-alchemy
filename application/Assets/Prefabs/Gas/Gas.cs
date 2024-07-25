using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Gas : MonoBehaviour
{
  private const float MAX_SPEED = 1f;

  private new Rigidbody2D rigidbody2D;

  private void EnterGasVolume(GasVolume gasVolume)
  {
    Debug.Log("Enter");
  }

  private void ExitGasVolume(GasVolume gasVolume)
  {
    Debug.Log("Exit");
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("Enter");
    if (LayerMask.LayerToName(other.gameObject.layer) == "GasVolume")
    {
      GasVolume gasVolume = other.gameObject.GetComponent<GasVolume>();
      EnterGasVolume(gasVolume);
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    Debug.Log("Exit");
    if (LayerMask.LayerToName(other.gameObject.layer) == "GasVolume")
    {
      GasVolume gasVolume = other.gameObject.GetComponent<GasVolume>();
      ExitGasVolume(gasVolume);
    }
  }

  private void Start()
  {
    rigidbody2D = GetComponent<Rigidbody2D>();
    rigidbody2D.velocity = RandomUtility.Vector2(0, MAX_SPEED);
  }
}
