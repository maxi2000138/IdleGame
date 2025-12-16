using Features.Farm;
using Features.Farming;
using Infrastructure.Camera;
using Input;
using Services;
using UnityEngine;
namespace Infrastructure.CompositionRoot
{
  public class CompositionRoot : MonoBehaviour
  {
    [SerializeField] private JoystickService _joystick;
    [SerializeField] private CameraService _camera;
    [SerializeField] private Configs _configs;
    [SerializeField] private Transform _characterSpawnPosition;
    [SerializeField] private Grass grass;

    private void Start()
    {
      var gameFactory = new GameFactory(_configs, _joystick, _camera, grass);
      
      InitServices();

      gameFactory.CreateCharacter(_characterSpawnPosition.position);
    }
    
    private void InitServices()
    {
      _joystick.Init();
      _joystick.Enable(true);
    }
  }
}