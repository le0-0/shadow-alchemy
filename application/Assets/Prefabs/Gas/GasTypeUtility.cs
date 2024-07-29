using System;

internal static class GasTypeUtility
{
  public static float GetDensity(this GasType type)
  {
    return type switch
    {
      GasType.Hydrogen => 0.0833f,
      GasType.Nitrogen => 1.1455f,
      GasType.Oxygen => 1.3088f,
      GasType.Air => 1.2041f,
      GasType.CarbonMonoxide => 1.1455f,
      GasType.CarbonDioxide => 1.8080f,
      GasType.Oxyhydrogen => 0.4126f,
      _ => throw new ArgumentException($"Density not defined for gas type: {type}"),
    };
  }
}
