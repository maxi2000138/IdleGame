using System;
using Features.Shop;
using Features.Units.Character;
using Features.Upgraders;
using Infrastructure.Camera;
using Infrastructure.Factory;
using Infrastructure.Input;
using UI;
using UnityEngine;

namespace Infrastructure.CompositionRoot
{
  public class CompositionRoot : MonoBehaviour
  {
    [SerializeField] private UiMediator _uiMediator;
    [SerializeField] private JoystickService _joystick;
    [SerializeField] private CameraService _camera;
    [SerializeField] private Configs _configs;
    [SerializeField] private Shop _shop;
    [SerializeField] private BackpackUpgrader _backpackUpgrader;
    [SerializeField] private HarvestRadiusUpgrader _harvestRadiusUpgrader;
    [SerializeField] private Transform _characterSpawnPosition;

    private void Start()
    {
      var gameFactory = new GameFactory(_configs, _joystick, _camera, _shop);
      _shop.Construct(gameFactory);

      var character = gameFactory.CreateCharacter(_characterSpawnPosition.position);
      _shop.SpawnCustomers();

      _uiMediator.Construct(character.Wallet, character.Inventory);
      _harvestRadiusUpgrader.Construct(_configs.UpgradersConfig);
      _backpackUpgrader.Construct(_configs.UpgradersConfig);

      _joystick.Init();
      _joystick.Enable(true);
    }
  }
}