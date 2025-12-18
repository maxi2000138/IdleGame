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
          new Bill.BillItem { Item = ItemType.GreenGrass, Amount =  Mathf.RoundToInt(level * 0.3f) },
          new Bill.BillItem { Item = ItemType.YellowGrass, Amount = Mathf.RoundToInt(level * 0.5f) },
        }
      };

    public int BackpackDeltaByLevel = 1;
    public Bill BackpackCostsByLevel(int level) =>
      new()
      {
        Items = new List<Bill.BillItem>()
        {
          new Bill.BillItem { Item = ItemType.GreenGrass, Amount =  Mathf.RoundToInt(level * 0.8f) },
          new Bill.BillItem { Item = ItemType.YellowGrass, Amount = level },
        }
      };
  }
}