using Features.Farm;
using Features.Farming;
using Features.Units.Character;
using Features.Units.Character.Inventory;
using Features.Units.Customer;
using Infrastructure.Camera;
using Infrastructure.CompositionRoot;
using Input;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services
{

  public class GameFactory : IGameFactory
  {
    private readonly Configs _configs;
    private readonly IJoystickService _joystick;
    private readonly ICameraService _camera;
    private Grass _grass;
    
    public GameFactory(Configs configs, IJoystickService joystick, ICameraService camera, Grass grass)
    {
      _configs = configs;
      _joystick = joystick;
      _camera = camera;
      _grass = grass;
    }

    public Character CreateCharacter(Vector3 position)
    {
      var prefab = _configs.CharacterConfig.Prefab;
      Character character = Object.Instantiate(prefab, position, Quaternion.identity);
      
      character.Mover.Construct(character, _joystick, _camera, _configs.CharacterConfig);
      character.Harvester.Construct(character, _configs.HarvesterConfig, this);
      character.Detector.Construct(character, _configs.CharacterConfig);
      character.Inventory.Construct(_configs.InventoryConfig.StartMaxCount, _configs.InventoryConfig.ItemHeight);
      
      character.Mover.ResetSpeed();
      _camera.SetTarget(character.transform);

      return character;
    }

    public Customer CreateCustomer(Vector3 position, Transform parent)
    {
      var prefab = _configs.CustomerConfig.Prefab;
      var customer = Object.Instantiate(prefab, position, Quaternion.identity);
      customer.transform.SetParent(parent);

      return customer;
    }
    public Item SpawnItem(GrassItem item, Vector3 worldPos)
    {
      return Object.Instantiate(item, worldPos, Quaternion.identity);
    }
  }
}