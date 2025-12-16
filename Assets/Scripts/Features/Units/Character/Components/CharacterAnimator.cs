using System.Collections;
using UnityEngine;
using Utils;

namespace Features.Units.Character
{
  public class CharacterAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;


    public void Move(float speed)
    {
      _animator.SetFloat(Animations.Velocity, speed);
    }
    
    public void Idle()
    {
      _animator.SetFloat(Animations.Velocity, 0f);
    }
    
    public void Harvest()
    {
      _animator.SetBool(Animations.Harvesting, true);
    }

    public void StopHarvest()
    {
      _animator.SetBool(Animations.Harvesting, false);
    }
  }
}