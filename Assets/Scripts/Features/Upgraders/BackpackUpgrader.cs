using Features.Units.Character;
using Features.Units.Character.Currency;
using Features.Upgraders.Configs;

namespace Features.Upgraders
{
  public class BackpackUpgrader : UpgraderBase
  {
    private int _currentLevel = 1;
    private UpgradersConfig _upgraderConfig;
    
    public void Construct(UpgradersConfig upgradersConfig)
    {
      _upgraderConfig = upgradersConfig;
      base.Construct(upgradersConfig.UpgradeDelayMS);
      
      UpdateUI();
    }
    
    public override void TryUpgrade(Character character)
    {
      if (character.Wallet.TryPayBill(_upgraderConfig.BackpackCostsByLevel(_currentLevel + 1)))
      { 
        _currentLevel++;
        character.Inventory.SetMaxCount(character.Inventory.MaxItemsCount + _upgraderConfig.BackpackDeltaByLevel);
        
        UpgraderUi.AnimateSuccess();
        UpdateUI();
      }
      else
      {
        UpgraderUi.AnimateNotEnoughMoney();
      }
    }
    
    private void UpdateUI()
    {
      UpgraderUi.SetValues($"Upgrade Backpack\nLevel {_currentLevel}", NextLevelBill());
      UpgraderUi.AnimateTextUpdate();
    }
    
    private Bill NextLevelBill() => _upgraderConfig.BackpackCostsByLevel(_currentLevel + 1);
  }
}