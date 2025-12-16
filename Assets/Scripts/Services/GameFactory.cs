using System;
using Features.Farm.Plant;
using Features.Units.Character;
using Features.Units.Character.Scythe;
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
    
    public GameFactory(Configs configs, IJoystickService joystick, ICameraService camera)
    {
      _configs = configs;
      _joystick = joystick;
      _camera = camera;
    }

    public Character CreateCharacter(Vector3 position)
    {
      var prefab = _configs.CharacterConfig.Prefab;
      Character character = Object.Instantiate(prefab, position, Quaternion.identity);
      
      character.Mover.Construct(character, _joystick, _camera, _configs.CharacterConfig);
      
      character.Mover.ResetSpeed();
      _camera.SetTarget(character.transform);

      return character;
    }
    
    public Plant CreatePlant(Vector3 position, Vector3 scale, Transform parent)
    {
      var prefab = _configs.FarmConfig.PlantPrefab;
      var plant = Object.Instantiate(prefab, position, Quaternion.identity, parent);
      plant.transform.localScale = scale;

      return plant;
    }

    public Scythe CreateScythe()
    {
      throw new NotImplementedException();
    }

    public PlantLoot CreateLoot(Vector3 position, Quaternion rotation, Transform parent)
    {
      var prefab = _configs.FarmConfig.LootPrefab;
      var lootComponent = Object.Instantiate(prefab, position, rotation, parent);

      return lootComponent;
    }

    public Customer CreateCustomer(Vector3 position, Transform parent)
    {
      var prefab = _configs.CustomerConfig.Prefab;
      var customer = Object.Instantiate(prefab, position, Quaternion.identity);
      customer.transform.SetParent(parent);

      return customer;
    }
  }
}