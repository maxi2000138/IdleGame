using Features.Units.Character.Scythe;
using UnityEngine;

namespace Features.Units.Character
{
  public class Character : MonoBehaviour
  {
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public CharacterMover Mover { get; private set; }
    [field: SerializeField] public CharacterDetector Detector { get; private set; }
    [field: SerializeField] public Harvester Harvester { get; private set; }
    [field: SerializeField] public CharacterAnimator Animator { get; private set; }
    [field: SerializeField] public Inventory.Inventory Inventory { get; private set; }

    
    private void Update()
    {
      Mover.MoveAndRotate();
      
      if (!Harvester.Enabled && Detector.DetectGrass(out var grass))
      {
        Harvester.Enable(grass);
        Animator.Harvest();
        Mover.SetSlowFactor(Harvester.SlowFactor);
      }
      else if (Harvester.Enabled && !Detector.DetectGrass(out _))
      {
        Harvester.Disable();
        Animator.StopHarvest();
        Mover.ResetSpeed();
      }
    }
  }
}