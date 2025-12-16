using Features.Units.Character;
using Features.Units.Customer;
using UnityEngine;

namespace Services
{
  public interface IGameFactory
  {
    Character CreateCharacter(Vector3 position);
    Customer CreateCustomer(Vector3 position, Transform parent);
  }
}