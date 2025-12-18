using System;
using System.Collections.Generic;
using Features.Units.Character.Inventory.Item;

namespace Features.Units.Character.Currency
{
  [Serializable]
  public class Bill
  {
    public List<BillItem> Items;
  
    [Serializable]
    public struct BillItem
    {
      public ItemType Item;
      public int Amount;
    }
  }
}