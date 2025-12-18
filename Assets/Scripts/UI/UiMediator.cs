using System.Collections.Generic;
using Features.Units.Character.Currency;
using Features.Units.Character.Inventory;
using UnityEngine;

namespace UI
{
  public class UiMediator : MonoBehaviour
  {
    public List<LootView> LootViews;
    public BackpackView BackpackView;

    private Currency _currency;
    private Inventory _inventory;

    public void Construct(Currency currency, Inventory inventory)
    {
      _inventory = inventory;
      _currency = currency;

      UpdateBackpack();
      UpdateCurrency();

      _currency.OnChange += UpdateCurrency;
      _inventory.OnChange += UpdateBackpack;
    }

    private void OnDestroy()
    {
      if (_currency != null) _currency.OnChange -= UpdateCurrency;
      if (_inventory != null) _inventory.OnChange -= UpdateBackpack;
    }

    private void UpdateBackpack()
    {
      BackpackView.SetValues(_inventory.ItemsAmount, _inventory.MaxItemsCount);
    }

    private void UpdateCurrency()
    {
      foreach (var lootView in LootViews)
      {
        lootView.SetText(_currency.Amount(lootView.ItemType).ToString());
      }
    }

  }
}