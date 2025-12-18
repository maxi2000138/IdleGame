using System;
using System.Collections.Generic;
using Features.Units.Character.Inventory;
using Features.Units.Character.Inventory.Item;
using UnityEngine;

namespace Features.Units.Character.Currency
{
  public class Wallet : MonoBehaviour
  {
    private readonly Dictionary<ItemType, int> _amount = new Dictionary<ItemType, int>();

    public event Action OnChange;
    
    public int Amount(ItemType itemType) => _amount.GetValueOrDefault(itemType, 0);
    
    public void Add(ItemType itemType, int amount)
    {
      if (!_amount.TryAdd(itemType, amount))
      {
        _amount[itemType] += amount;
        OnChange?.Invoke();
      }
    }

    public bool CanPayBill(Bill bill)
    {
      foreach (var billItem in bill.Items)
      {
        if (_amount[billItem.Item] < billItem.Amount)
          return false;
      }

      return true;
    }

    public bool TryPayBill(Bill bill)
    {
      if (!CanPayBill(bill)) return false;

      foreach (var billItem in bill.Items) 
        _amount[billItem.Item] -= billItem.Amount;

      OnChange?.Invoke();
      return true;
    }
  }
}