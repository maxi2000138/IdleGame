using Features.Farm.Plant;
using Features.Farm.Soil;
using Features.Units.Character;
using Features.Units.Character.Scythe;
using Features.Units.Customer;
using UnityEngine;

namespace Services
{
  public interface IGameFactory
  {
    Character CreateCharacter(Vector3 position);
    Soil CreateSoil(Vector3 position, Vector3 scale, Transform parent);
    Plant CreatePlant(Vector3 position, Vector3 scale, Transform parent);
    Scythe CreateScythe();
    PlantLoot CreateLoot(Vector3 position, Quaternion rotation, Transform parent);
    Customer CreateCustomer(Vector3 position, Transform parent);
  }
}