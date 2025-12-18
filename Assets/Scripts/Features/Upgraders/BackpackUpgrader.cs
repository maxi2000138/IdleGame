using Features.Units.Character;
using Features.Units.Character.Currency;
using Features.Upgraders.Configs;

namespace Features.Upgraders
{
  public class BackpackUpgrader : UpgraderBase
  {
    private int _currentLevel = 1;
    private UpgradersConfig _upgradersConfig;
    
    public void Construct(UpgradersConfig upgradersConfig)
    {
      _upgradersConfig = upgradersConfig;
      base.Construct(upgradersConfig.UpgradeDelayMS);
      
      UpdateUI();
    }
    
    public override void TryUpgrade(Character character)
    {
      if (character.Wallet.TryPayBill(_upgradersConfig.BackpackCostsByLevel(_currentLevel + 1)))
      { 
        _currentLevel++;
        character.Inventory.SetMaxCount(character.Inventory.MaxItemsCount + _upgradersConfig.BackpackDeltaByLevel);
        
        UpdateUI();
      }
    }
    
    private void UpdateUI() => UpgraderUi.SetValues($"Upgrade Backpack\nLevel {_currentLevel}", NextLevelBill());
    private Bill NextLevelBill() => _upgradersConfig.BackpackCostsByLevel(_currentLevel + 1);
  }
}