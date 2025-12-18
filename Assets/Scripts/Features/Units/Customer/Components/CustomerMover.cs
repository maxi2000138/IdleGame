using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Features.Shop;
using UnityEngine;
using Utils;
using Utils.Extensions;

namespace Features.Units.Customer.Components
{
  public class CustomerMover : MonoBehaviour
  {
    [SerializeField] private AnimationConfig _animationsConfig;

    private CustomerQueue _customerQueue;

    public void Construct(CustomerQueue customerQueue)
    {
      _customerQueue = customerQueue;
    }

    public async UniTask GetAway(Customer customer)
    {
      await RunToEndField(customer);
      await Jump(customer);
      Destroy(customer);
    }


    public void RotateForward()
    {
      transform.DORotate(CustomerConstants.ForwardRotation, _animationsConfig.BuyLootData.RotateDuration).SetEase(Ease.InOutQuad);
    }

    public async UniTask CatchLoot(Customer customer)
    {
      await UniTask.Delay(_animationsConfig.BuyLootData.DelayBeforeBuy.ToMS());
      customer.CustomerAnimator.Catch();
      await UniTask.Delay(_animationsConfig.BuyLootData.CatchDelay.ToMS());
    }

    private async UniTask Jump(Customer customer)
    {
      customer.Agent.enabled = false;

      var endValue = customer.transform.position + customer.transform.forward * _animationsConfig.BuyLootData.JumpDistance.z;
      endValue.y += _animationsConfig.BuyLootData.JumpDistance.y;

      customer.transform
        .DOJump(endValue, _animationsConfig.BuyLootData.JumpForce, 1, _animationsConfig.BuyLootData.JumpDuration)
        .SetEase(Ease.InOutQuad);

      customer.CustomerAnimator.Flip();
      await UniTask.Delay(2000);
    }

    private async UniTask RunToEndField(Customer customer)
    {
      customer.Agent.SetDestination(_customerQueue.FinalPoint.position);
      customer.CustomerAnimator.Run();
      await UniTask.WaitWhile(() => customer.Agent.hasPath);
    }


    [Serializable]
    public class AnimationConfig
    {
      public BuyLoot BuyLootData;

      [Serializable]
      public class BuyLoot
      {
        [Range(0f, 3f)]
        public float DelayBeforeBuy;
        [Range(0f, 3f)]
        public float CatchDelay;
        [Range(0f, 10f)]
        public float JumpForce;
        [Range(0f, 3f)]
        public float JumpDuration;
        public Vector3 JumpDistance;
        [Range(0f, 2f)]
        public float RotateDuration;
      }
    }
  }
}