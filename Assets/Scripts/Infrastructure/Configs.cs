using Features.Farm._Config;
using Features.Units.Character._Config;
using Features.Units.Character.Inventory._Config;
using Features.Units.Character.Scythe._Config;
using Features.Units.Customer._Configs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.CompositionRoot
{
  public class Configs : MonoBehaviour
  {
    public CharacterConfig CharacterConfig;
    [Space(20)] public GrassConfig grassConfig;
    [Space(20)] public CustomerConfig CustomerConfig;
    [Space(20)] public HarvesterConfig HarvesterConfig;
    [Space(20)] public InventoryConfig InventoryConfig;
  }
}