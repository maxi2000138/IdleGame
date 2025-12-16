using Features.Farm._Config;
using Features.Units.Character._Config;
using Features.Units.Character.Scythe._Config;
using Features.Units.Customer._Configs;
using UnityEngine;
namespace Infrastructure.CompositionRoot
{
  public class Configs : MonoBehaviour
  {
    public CharacterConfig CharacterConfig;
    [Space(20)] public FarmConfig FarmConfig;
    [Space(20)] public CustomerConfig CustomerConfig;
    [Space(20)] public ScytheConfig ScytheConfig;
  }
}