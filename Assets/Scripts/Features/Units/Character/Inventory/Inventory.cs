using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utils;
using Utils.Extensions;

namespace Features.Units.Character.Inventory
{
  public class Inventory : MonoBehaviour
  {
    public Transform BackpackSlot;
    [SerializeField] private AnimationConfig _animationConfig;

    private int _maxCount;
    private float _itemHeight;
    private Stack<Item.Item> _items;

    public event Action OnChange;

    public int ItemsAmount => _items.Count;
    public int MaxItemsCount => _maxCount;
    public bool CanPushItem => _items.Count < _maxCount;
    public Vector3 NewItemPosition => BackpackSlot.localPosition.AddY(_items.Count * _itemHeight);
    public bool HasItems => _items.Count > 0;

    public void Construct(int maxCount, float itemHeight)
    {
      _itemHeight = itemHeight;
      _items = new Stack<Item.Item>();
      SetMaxCount(maxCount);
    }

    public void SetMaxCount(int maxCount)
    {
      _maxCount = maxCount;
      OnChange?.Invoke();
    }

    public bool TryPushItem(Item.Item item)
    {
      if (_items.Count >= _maxCount)
        return false;

      AnimateItemToInventory(item, NewItemPosition);
      _items.Push(item);
      OnChange?.Invoke();

      return true;
    }

    public bool TryPopItem(out Item.Item item)
    {
      if (HasItems)
      {
        item = _items.Pop();
        OnChange?.Invoke();
        return true;
      }

      item = null;
      return false;
    }

    public void DetachItemFromBackpack(Item.Item item)
    {
      item.transform.SetParent(null);
    }

    private void AnimateItemToInventory(Item.Item item, Vector3 targetPosition)
    {
      item.transform.SetParent(BackpackSlot);
      item.transform.DOLocalJump(targetPosition, _animationConfig.JumpHeight,
        _animationConfig.JumpCount, _animationConfig.JumpDuration);
    }


    [Serializable]
    public class AnimationConfig
    {
      public float JumpHeight = 1f;
      public int JumpCount = 1;
      public float JumpDuration = 0.5f;
    }
  }
}