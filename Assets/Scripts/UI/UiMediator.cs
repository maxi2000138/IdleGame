using System.Collections.Generic;
using Features.Units.Character.Currency;
using Features.Units.Character.Inventory;
using Features.Units.Character.Inventory.Item;
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

      SetupBackpack();
      SetupWallet();
      
      _wallet.OnChange += UpdateWallet;
      _inventory.OnChange += UpdateBackpack;
    }

    private void OnDestroy()
    {
      if (_wallet != null) _wallet.OnChange -= UpdateWallet;
      if (_inventory != null) _inventory.OnChange -= UpdateBackpack;
    }
    
    
    private void SetupBackpack()
    {
      BackpackView.SetValues(_inventory.ItemsAmount, _inventory.MaxItemsCount);
    }

    private void UpdateBackpack()
    {
      BackpackView.SetValues(_inventory.ItemsAmount, _inventory.MaxItemsCount);
      BackpackView.AnimateChange();
    }
    
    private void SetupWallet()
    {
      foreach (var lootView in LootViews)
      {
        lootView.SetText(WalletItemText(lootView));
      }
    }

    private void UpdateWallet(ItemType itemType)
    {
      foreach (var lootView in LootViews)
      {
        if (lootView.ItemType != itemType) continue;
        
        lootView.SetText(WalletItemText(lootView));
        lootView.AnimateChange();
      }
    }
    
    private string WalletItemText(LootView lootView) => _wallet.Amount(lootView.ItemType).ToString();
  }
}