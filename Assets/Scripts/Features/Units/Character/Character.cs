using System.Threading;
using Cysharp.Threading.Tasks;
using Features.Units.Character.Components;
using Features.Units.Character.Harvest;
using UnityEngine;
using CharacterController = Features.Units.Character.Components.CharacterController;

namespace Features.Units.Character
{
  public class Character : MonoBehaviour
  {
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public CharacterMover Mover { get; private set; }
    [field: SerializeField] public CharacterDetector Detector { get; private set; }
    [field: SerializeField] public Harvester Harvester { get; private set; }
    [field: SerializeField] public Inventory.Inventory Inventory { get; private set; }
    [field: SerializeField] public Currency.Currency Currency { get; private set; }
    [field: SerializeField] public CharacterAnimator Animator { get; private set; }
    [field: SerializeField] public CharacterSeller Seller { get; private set; }

    private CancellationTokenSource _cancellationSource;
    private UniTaskCompletionSource _tradingTask;


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

      if (Detector.DetectShop(out var shop) && Mover.IsIdle && !Trading)
      {
        _cancellationSource = new CancellationTokenSource();
        Trade(shop, _cancellationSource.Token).Forget();
      }
      else if (ShouldStopTrade())
      {
        StopTrading();
      }
    }

    private async UniTaskVoid Trade(Shop.Shop shop, CancellationToken cancellationToken)
    {
      _tradingTask = new UniTaskCompletionSource();

      while (shop.HasCustomers)
      {
        if (cancellationToken.IsCancellationRequested || !Inventory.HasItems)
          break;

        var customer = await shop.WaitForCustomer();
        Inventory.TryPopItem(out var item);
        Seller.SellAndThrowLootToShop(item).Forget();
        await customer.CatchLoot();
        shop.UpdateAllCustomersPosition().Forget();
        customer.GetAway().Forget();
      }

      _tradingTask?.TrySetResult();
      _tradingTask = null;
    }

    private void StopTrading()
    {
      _cancellationSource?.Cancel();
      _cancellationSource = null;
    }

    private bool ShouldStopTrade() => Trading && (!Detector.DetectShop(out _) || !Mover.IsIdle);
    private bool Trading => _tradingTask != null;
  }
}