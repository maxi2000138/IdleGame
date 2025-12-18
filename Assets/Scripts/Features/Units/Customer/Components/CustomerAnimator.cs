using UnityEngine;
using Utils;

namespace Features.Units.Customer.Components
{
  public class CustomerAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;

    public void Run()
    {
      _animator.SetFloat(Animations.Velocity, 1f);
    }

    public void Idle()
    {
      _animator.SetFloat(Animations.Velocity, 0f);
    }

    public void Flip()
    {
      _animator.Play(Animations.Flip);
    }

    public void Catch()
    {
      _animator.CrossFade(Animations.Catch, 0.2f);
    }
  }
}