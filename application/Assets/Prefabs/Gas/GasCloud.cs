using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GasCloud : MonoBehaviour
{
  private const float BUOYANCY_DEADZONE = 0.05f;

  private const float MAX_SPEED = 1f;

  private const float MIN_SPEED = 0.1f;

  /// <summary>
  /// The maximum number of <see cref="GasCloud"/>s that can make up the neighborhood of a <see cref="GasCloud"/>.
  /// </summary>
  private const int NEIGHBOR_MAX_COUNT = 20;

  /// <summary>
  /// The square of the max distance away a <see cref="GasCloud"/> can be to be inside the neighborhood of a <see cref="GasCloud"/>
  /// </summary>
  private const float NEIGHBOR_MAX_SQUARED_DISTANCE = 1f;

  /// <summary>
  /// If the number of <see cref="GasCloud"/>s in the neighborhood is smaller than this count, it will be filled with reference air.
  /// </summary>
  private const float NEIGHBOR_MIN_COUNT = 10;

  private static readonly HashSet<GasCloud> gasCloud = new HashSet<GasCloud>();

  private static IEnumerable<GasCloud> GetNeighborhood(Vector3 center)
  {
    return gasCloud
      .Select(gasCloud => new { gasCloud, squareDistance = (gasCloud.transform.position - center).sqrMagnitude })
      .Where(item => item.squareDistance < NEIGHBOR_MAX_SQUARED_DISTANCE)
      .OrderBy(item => item.squareDistance)
      .Take(NEIGHBOR_MAX_COUNT)
      .Select(item => item.gasCloud);
  }

  [SerializeField]
  private GasType gasType;

  private new Rigidbody2D rigidbody2D;

  private void FixedUpdate()
  {
    ProcessBuoyancy();
    ProcessVelocity();
  }

  private void ProcessBuoyancy()
  {
    List<GasCloud> neighborGasClouds = GetNeighborhood(transform.position).ToList();
    float neighborhoodDensity = neighborGasClouds.Select(gasCloud => gasCloud.gasType.GetDensity()).Average();

    if (neighborGasClouds.Count < NEIGHBOR_MIN_COUNT)
    {
      float prev = neighborhoodDensity;
      float backfillVolume = NEIGHBOR_MIN_COUNT - neighborGasClouds.Count;
      neighborhoodDensity =
        (neighborGasClouds.Count * neighborhoodDensity + backfillVolume * GasType.Air.GetDensity())
        / NEIGHBOR_MIN_COUNT;
      Debug.Log(
        $"Prev: {prev}; Count: {neighborGasClouds.Count}; Air: {GasType.Air.GetDensity()}; New: {neighborhoodDensity}"
      );
    }

    float densityDifference = gasType.GetDensity() - neighborhoodDensity;
    Vector2 buoyancyForce =
      MathF.Sign(densityDifference)
      * MathF.Max(0, MathF.Abs(densityDifference) - BUOYANCY_DEADZONE)
      * Physics2D.gravity;

    rigidbody2D.AddForce(buoyancyForce, ForceMode2D.Force);
  }

  private void ProcessVelocity()
  {
    if (MAX_SPEED * MAX_SPEED < rigidbody2D.velocity.sqrMagnitude)
    {
      rigidbody2D.velocity = MAX_SPEED * rigidbody2D.velocity.normalized;
    }

    if (rigidbody2D.velocity.sqrMagnitude < MIN_SPEED * MIN_SPEED)
    {
      rigidbody2D.velocity = MIN_SPEED * rigidbody2D.velocity.normalized;
    }
  }

  private void OnDisable()
  {
    gasCloud.Remove(this);
  }

  private void OnEnable()
  {
    gasCloud.Add(this);
  }

  private void Start()
  {
    rigidbody2D = GetComponent<Rigidbody2D>();
    rigidbody2D.velocity = RandomUtility.Vector2(0, MAX_SPEED);
  }
}
