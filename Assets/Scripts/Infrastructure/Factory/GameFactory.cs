using Features.Grass;
using Features.Shop;
using Features.Units.Character;
using Features.Units.Character.Inventory.Item;
using Features.Units.Customer;
using Infrastructure.Camera;
using Infrastructure.Input;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace Infrastructure.Factory
{

  public class GameFactory : IGameFactory
  {
    private readonly Configs _configs;
    private readonly IJoystickService _joystick;
    private readonly ICameraService _camera;
    private readonly Shop _shop;

    public GameFactory(Configs configs, IJoystickService joystick, ICameraService camera, Shop shop)
    {
      _configs = configs;
      _joystick = joystick;
      _camera = camera;
      _shop = shop;
    }

    public Character CreateCharacter(Vector3 position)
    {
      var prefab = _configs.CharacterConfig.Prefab;
      var character = Object.Instantiate(prefab, position, Quaternion.identity);

      character.Mover.Construct(character, _joystick, _camera, _configs.CharacterConfig);
      character.Harvester.Construct(character, _configs.HarvesterConfig, this);
      character.Detector.Construct(character, _configs.CharacterConfig);
      character.Inventory.Construct(_configs.InventoryConfig.StartMaxCount, _configs.InventoryConfig.ItemHeight);
      character.Seller.Construct(character.Inventory, character.Animator, _shop, character.Currency);

      character.Mover.ResetSpeed();
      _camera.SetTarget(character.transform);

      return character;
    }

    public Customer CreateCustomer(Vector3 position, Transform parent)
    {
      var prefab = _configs.CustomerConfig.Prefab;
      var customer = Object.Instantiate(prefab, position, Quaternion.identity);
      customer.CustomerMover.Construct(_shop.CustomerQueue);
      customer.transform.eulerAngles = CustomerConstants.ForwardRotation;
      customer.transform.SetParent(parent);

      return customer;
    }

    public Item SpawnItem(GrassItem item, Vector3 worldPos) => Object.Instantiate(item, worldPos, Quaternion.identity);
  }
}