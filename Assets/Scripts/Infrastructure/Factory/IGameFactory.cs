using Features.Grass;
using Features.Units.Character;
using Features.Units.Character.Inventory.Item;
using Features.Units.Customer;
using UnityEngine;

namespace Infrastructure.Factory
{
  public interface IGameFactory
  {
    Character CreateCharacter(Vector3 position);
    Customer CreateCustomer(Vector3 position, Transform parent);
    Item SpawnItem(GrassItem item, Vector3 worldPos);
  }
}