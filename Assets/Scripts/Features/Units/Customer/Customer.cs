using Cysharp.Threading.Tasks;
using Features.Units.Customer.Components;
using UnityEngine;
using UnityEngine.AI;

namespace Features.Units.Customer
{
  public class Customer : MonoBehaviour
  {
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public CustomerMover CustomerMover { get; private set; }
    [field: SerializeField] public CustomerAnimator CustomerAnimator { get; private set; }

    public async UniTask CatchLoot()
    {
      await CustomerMover.CatchLoot(this);
    }

    public async UniTask GetAway()
    {
      await CustomerMover.GetAway(this);
    }
  }
}