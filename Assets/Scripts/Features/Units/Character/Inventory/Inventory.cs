using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Features.Units.Character.Inventory
{
  public class Inventory : MonoBehaviour
  {
    public Transform BackpackSlot;
    [SerializeField] private AnimationConfig _animationConfig;
    
    private int _maxCount;
    private float _itemHeight;
    private Stack<Item> _items;
    
    public bool CanPushItem => _items.Count < _maxCount;
    public Vector3 NewItemPosition => BackpackSlot.localPosition.AddY(_items.Count * _itemHeight);

    public void Construct(int maxCount, float itemHeight)
    {
      _itemHeight = itemHeight;
      _maxCount = maxCount;
      _items = new Stack<Item>();
    }
    
    public void SetMaxCount(int maxCount)
    {
      _maxCount = maxCount;
    }

    public bool TryPushItem(Item item)
    {
      if (_items.Count >= _maxCount)
        return false;
      
      AnimateItemToInventory(item, NewItemPosition);
      _items.Push(item);
      
      return true;
    }
    
    public bool TryPopItem(out Item item)
    {
      if (_items.Count > 0)
      {
        item = _items.Pop(); 
        DetachItemFromBackpack(item); 
        return true;
      }
      
      item = null;
      return false;
    }
    
    private void AnimateItemToInventory(Item item, Vector3 targetPosition)
    {
      item.transform.SetParent(BackpackSlot);
      item.transform.DOLocalJump(targetPosition, _animationConfig.JumpHeight, 
          _animationConfig.JumpCount, _animationConfig.JumpDuration);
    }
    
    private void DetachItemFromBackpack(Item item)
    {
      item.transform.SetParent(null);
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