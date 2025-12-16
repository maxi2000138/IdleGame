using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Units.Character
{
  public class Character : MonoBehaviour
  {
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public CharacterMover Mover { get; private set; }
    [field: FormerlySerializedAs("<CharacterAnimator>k__BackingField")] [field: SerializeField] public CharacterAnimator Animator { get; private set; }
  }
}