using UnityEngine;

internal class Vitality : MonoBehaviour
{
  [SerializeField]
  private Event deathEvent;

  [SerializeField]
  private float vitality = 0;

  public void DealDamage(float damage)
  {
    vitality -= damage;

    if (0 < vitality)
      return;

    deathEvent.Invoke();
    enabled = false;
  }
}
