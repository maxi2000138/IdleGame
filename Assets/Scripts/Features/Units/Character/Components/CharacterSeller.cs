using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Features.Units.Character.Inventory.Item;
using UnityEngine;

namespace Features.Units.Character.Components
{
  public class CharacterSeller : MonoBehaviour
  {
    [SerializeField] private Transform _sellPoint;
    [SerializeField] private AnimationsConfig _animationsConfig;

    private Inventory.Inventory _inventory;
    private Shop.Shop _shop;
    private CharacterAnimator _animator;
    private Currency.Wallet _wallet;

    public void Construct(Inventory.Inventory inventory, CharacterAnimator animator, Shop.Shop shop, Currency.Wallet wallet)
    {
      _wallet = wallet;
      _animator = animator;
      _shop = shop;
      _inventory = inventory;
    }

    public async UniTask SellAndThrowLootToShop(Item item)
    {
      await ThrowItem(item);
      AddCurency(item);
      await RemoveItem(item);
    }

    private async UniTask ThrowItem(Item item)
    {
      _animator.Throw();
      await item.transform.DOLocalMove(_sellPoint.localPosition, _animationsConfig.BackToArmsDuration).ToUniTask();
      _inventory.DetachItemFromBackpack(item);
      await item.transform.DOJump(_shop.SellPoint.position, _animationsConfig.JumpForce,
        1, _animationsConfig.JumpDuration).ToUniTask();
    }

    private async UniTask RemoveItem(Item item)
    {
      await item.transform.DOScale(Vector3.zero, _animationsConfig.DissapearDuration);
      Destroy(item.gameObject);
    }


    private void AddCurency(Item item) => _wallet.Add(item.Type, 1);


    [Serializable]
    public class AnimationsConfig
    {
      [Range(0f, 3f)]
      public float JumpForce;
      [Range(0f, 3f)]
      public float JumpDuration;
      [Range(0f, 3f)]
      public float BackToArmsDuration;
      [Range(0f, 3f)]
      public float DissapearDuration;
    }
  }
}