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

    private Wallet _wallet;
    private Inventory _inventory;

    public void Construct(Wallet wallet, Inventory inventory)
    {
      _inventory = inventory;
      _wallet = wallet;

      UpdateBackpack();
      UpdateWallet();

      _wallet.OnChange += UpdateWallet;
      _inventory.OnChange += UpdateBackpack;
    }

    private void OnDestroy()
    {
      if (_wallet != null) _wallet.OnChange -= UpdateWallet;
      if (_inventory != null) _inventory.OnChange -= UpdateBackpack;
    }

    private void UpdateBackpack()
    {
      BackpackView.SetValues(_inventory.ItemsAmount, _inventory.MaxItemsCount);
    }

    private void UpdateWallet()
    {
      foreach (var lootView in LootViews)
      {
        lootView.SetText(_wallet.Amount(lootView.ItemType).ToString());
      }
    }

  }
}