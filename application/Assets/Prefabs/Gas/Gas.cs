using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Gas : MonoBehaviour
{
  private const float MAX_SPEED = 1f;

  [SerializeField]
  private GasType gasType;

  private new Rigidbody2D rigidbody2D;
  private GasVolume gasVolume;

  private void EnterGasVolume(GasVolume gasVolume)
  {
    this.gasVolume = gasVolume;
    gasVolume.CloudsByGasType[gasType].Add(this);
  }

  private void ExitGasVolume(GasVolume gasVolume)
  {
    if (this.gasVolume == gasVolume)
    {
      // Respawn in same volume.
    }
    else
    {
      // Remove itself from its former volume because the cloud has entered another volume.
      gasVolume.CloudsByGasType[gasType].Remove(this);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (LayerMask.LayerToName(other.gameObject.layer) == "GasVolume")
    {
      GasVolume gasVolume = other.gameObject.GetComponent<GasVolume>();
      EnterGasVolume(gasVolume);
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
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
