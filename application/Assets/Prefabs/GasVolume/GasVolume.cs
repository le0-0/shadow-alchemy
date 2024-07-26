using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
internal class GasVolume : MonoBehaviour
{
  /// <summary>
  /// The number of clouds that exists but are hidden in a <see cref="GasVolume"/> with normal air.
  /// </summary>
  private const int IMPLIED_CLOUDS = 100;

  public IReadOnlyDictionary<GasType, HashSet<Gas>> CloudsByGasType { get; private set; }
  public IReadOnlyDictionary<GasType, float> ConcentrationByGasType { get; private set; }
  public float Density { get; private set; }

  private Dictionary<GasType, float> concentrationByGasType;

  private void Awake()
  {
    CloudsByGasType = Enum.GetValues(typeof(GasType))
      .Cast<GasType>()
      .ToDictionary(gasType => gasType, _ => new HashSet<Gas>());

    concentrationByGasType = new Dictionary<GasType, float>();
    ResetGasConcentrations();
    ConcentrationByGasType = concentrationByGasType;

    UpdateDensity();
  }

  private void FixedUpdate()
  {
    UpdateConcentrationsByGasType();
    UpdateDensity();
  }

  private void ResetGasConcentrations()
  {
    foreach (GasType gasType in Enum.GetValues(typeof(GasType)))
    {
      concentrationByGasType[gasType] = 0f;
    }
    concentrationByGasType[GasType.Nitrogen] = 0.79f;
    concentrationByGasType[GasType.Oxygen] = 0.21f;
  }

  private void UpdateConcentrationsByGasType()
  {
    int totalGasCount = CloudsByGasType.Values.Sum(gasSet => gasSet.Count);

    if (totalGasCount == 0)
    {
      ResetGasConcentrations();
      return;
    }

    foreach (GasType gasType in Enum.GetValues(typeof(GasType)))
    {
      int gasTypeCount = CloudsByGasType[gasType].Count;
      float concentration = (float)gasTypeCount / totalGasCount;
      concentrationByGasType[gasType] = concentration;
    }
  }

  private void UpdateDensity()
  {
    Density = Enum.GetValues(typeof(GasType))
      .Cast<GasType>()
      .Select(gasType => gasType.GetDensity() * concentrationByGasType[gasType])
      .Sum();
  }
}
