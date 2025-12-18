using Features.Units.Character._Config;
using Features.Units.Character.Harvest._Config;
using Features.Units.Character.Inventory._Config;
using Features.Units.Customer._Configs;
using UnityEngine;

namespace Infrastructure
{
  public class Configs : MonoBehaviour
  {
    public CharacterConfig CharacterConfig;
    [Space(20)] public CustomerConfig CustomerConfig;
    [Space(20)] public HarvesterConfig HarvesterConfig;
    [Space(20)] public InventoryConfig InventoryConfig;
  }
}