using Features.Units.Character;
using Features.Units.Character.Currency;
using Features.Upgraders.Configs;

namespace Features.Upgraders
{
  public class HarvestRadiusUpgrader : UpgraderBase
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
      if (character.Wallet.TryPayBill(_upgradersConfig.HarvestScaleCostsByLevel(_currentLevel + 1)))
      {
        _currentLevel++;
        character.Harvester.SetHarvestScale(character.Harvester.HarvestScale + _upgradersConfig.HarvestScaleDeltaByLevel);
        
        UpdateUI();
      }
    }
    
    private void UpdateUI() => UpgraderUi.SetValues($"Upgrade Harvest Radius\nLevel {_currentLevel}", NextLevelBill());
    private Bill NextLevelBill() => _upgradersConfig.HarvestScaleCostsByLevel(_currentLevel + 1);
  }
}