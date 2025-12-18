using UnityEngine;

namespace Features.Units.Character.Components
{
  public class CharacterController : MonoBehaviour
  {
    [SerializeField] private UnityEngine.CharacterController _characterController;

    public UnityEngine.CharacterController Controller => _characterController;
    public float Angle => transform.eulerAngles.y;
    public float Speed { get; private set; }

    public void SetSpeed(float speed) => Speed = speed;
  }
}