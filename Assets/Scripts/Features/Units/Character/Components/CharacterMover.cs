using Features.Units.Character._Config;
using Infrastructure.Camera;
using Infrastructure.Input;
using UnityEngine;

namespace Features.Units.Character.Components
{
  public class CharacterMover : MonoBehaviour
  {
    private IJoystickService _joystickService;
    private CharacterConfig _characterConfig;
    private ICameraService _cameraService;
    private Character _character;

    private const float RayDistance = 5f;
    private const float LerpRotate = 0.25f;

    private float JoystickAngle => Mathf.Atan2(_joystickService.GetAxis().x,
                                     _joystickService.GetAxis().y) *
                                   Mathf.Rad2Deg +
                                   _cameraService.Camera.transform.eulerAngles.y;

    public bool IsIdle => _joystickService.GetAxis() == Vector2.zero;

    public void Construct(Character character, IJoystickService joystickService,
      ICameraService cameraService, CharacterConfig characterConfig)
    {
      _cameraService = cameraService;
      _character = character;
      _joystickService = joystickService;
      _characterConfig = characterConfig;
    }

    public void MoveAndRotate()
    {
      if (_joystickService.GetAxis() != Vector2.zero)
      {
        Move();
        Rotate();
      }
      else
      {
        Idle();
      }
    }

    private void Move()
    {
      var move = Quaternion.Euler(0f, JoystickAngle, 0f) * Vector3.forward;

      var next = _character.transform.position + move * (_character.CharacterController.Speed * Time.deltaTime);

      var ray = new Ray
      {
        origin = next,
        direction = Vector3.down,
      };
      if (!Physics.Raycast(ray, RayDistance)) return;

      _character.CharacterController.Controller.Move(move * (_character.CharacterController.Speed * Time.deltaTime));
      _character.Animator.Move(1f);
    }

    private void Idle()
    {
      _character.Animator.Idle();
    }


    private void Rotate()
    {
      var lerpAngle = Mathf.LerpAngle(_character.CharacterController.Angle, JoystickAngle, LerpRotate);
      _character.CharacterController.transform.rotation = Quaternion.Euler(0f, lerpAngle, 0f);
    }

    public void SetSlowFactor(float factor) =>
      _character.CharacterController.SetSpeed(_characterConfig.Speed * factor);

    public void ResetSpeed() =>
      _character.CharacterController.SetSpeed(_characterConfig.Speed);
  }
}