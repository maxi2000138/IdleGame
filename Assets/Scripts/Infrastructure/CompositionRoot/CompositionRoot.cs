using Features.Farm;
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
    [SerializeField] private Farm _farm;

    private void Start()
    {
      IGameFactory gameFactory = new GameFactory(_configs, _joystick, _camera);
      _farm.Construct(gameFactory, _configs.FarmConfig);
      
      InitServices();

      _farm.CreateFarmField();
      gameFactory.CreateCharacter(_characterSpawnPosition.position);
    }
    
    private void InitServices()
    {
      _joystick.Init();
      _joystick.Enable(true);
    }
  }
}