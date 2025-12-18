using System;
using System.Collections.Generic;
using Features.Units.Character.Inventory;
using Features.Units.Character.Inventory.Item;
using UnityEngine;

namespace Features.Units.Character.Currency
{
  public class Currency : MonoBehaviour
  {
    private readonly Dictionary<ItemType, int> _amount = new Dictionary<ItemType, int>();

    public event Action OnChange;

    public void Add(ItemType itemType, int amount)
    {
      if (!_amount.TryAdd(itemType, amount))
      {
        _amount[itemType] += amount;
        OnChange?.Invoke();
      }
    }

    public bool Spend(ItemType itemType, int amount)
    {
      if (!_amount.TryGetValue(itemType, out var value))
        return false;

      if (value >= amount)
      {
        _amount[itemType] -= amount;
        OnChange?.Invoke();
        return true;
      }

      return false;
    }

    public int Amount(ItemType itemType)
    {
      if (!_amount.TryGetValue(itemType, out var value))
        return 0;

      return value;
    }
  }
}