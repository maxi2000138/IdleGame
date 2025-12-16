using UnityEngine;

namespace Features.Units.Character
{
  public class CharacterAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;

    private readonly int _velocityHash = Animator.StringToHash("Velocity");
    private readonly int _harvestSpeed = Animator.StringToHash("HarvestSpeed");

    public void Move(float speed)
    {
      _animator.SetFloat(_velocityHash, speed);
    }
    
    public void Idle()
    {
      _animator.SetFloat(_velocityHash, 0f);
    }
  }
}