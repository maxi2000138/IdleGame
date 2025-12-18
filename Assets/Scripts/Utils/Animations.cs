using UnityEngine;

namespace Utils
{
  public static class Animations
  {
    public static int Harvesting = Animator.StringToHash(nameof(Harvesting));
    public static int Velocity = Animator.StringToHash(nameof(Velocity));
    public static int Catch = Animator.StringToHash(nameof(Catch));
    public static int Throw = Animator.StringToHash(nameof(Throw));
    public static int Flip = Animator.StringToHash(nameof(Flip));
  }
}