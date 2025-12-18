using System;
using System.Collections.Generic;
using Features.Units.Character.Currency;
using Features.Units.Character.Inventory.Item;
using UnityEngine;

namespace Features.Upgraders.Configs
{
  [Serializable]
  public class UpgradersConfig
  {
    public int UpgradeDelayMS = 1000;
    
    public float HarvestScaleDeltaByLevel = 0.1f;
    public Bill HarvestScaleCostsByLevel(int level) =>
      new()
      {
        Items = new List<Bill.BillItem>()
        {
          new Bill.BillItem { Item = ItemType.GreenGrass, Amount = level * 2 },
          new Bill.BillItem { Item = ItemType.YellowGrass, Amount = Mathf.RoundToInt(level * 1.5f) },
        }
      };

    public int BackpackDeltaByLevel = 1;
    public Bill BackpackCostsByLevel(int level) =>
      new()
      {
        Items = new List<Bill.BillItem>()
        {
          new Bill.BillItem { Item = ItemType.GreenGrass, Amount = level },
          new Bill.BillItem { Item = ItemType.YellowGrass, Amount = level * 3 },
        }
      };
  }
}