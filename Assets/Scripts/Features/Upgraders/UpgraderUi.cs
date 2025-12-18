using System.Collections.Generic;
using System.Linq;
using Features.Units.Character.Currency;
using TMPro;
using UI;
using UnityEngine;

namespace Features.Upgraders
{
  public class UpgraderUi : MonoBehaviour
  {
    public TextMeshProUGUI MainText;
    public List<LootView> LootViews;
    
    public void SetValues(string mainText, Bill bill)
    {
      MainText.text = mainText;
      
      for (int i = 0; i < LootViews.Count; i++)
      {
        var item = bill.Items.First(x => x.Item == LootViews[i].ItemType);
        LootViews[i].SetText(item.Amount.ToString());
      }
    }
  }
}