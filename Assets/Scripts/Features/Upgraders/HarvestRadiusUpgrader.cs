using Features.Units.Character;
using Features.Units.Character.Currency;
using Features.Upgraders.Configs;

namespace Features.Upgraders
{
  public class HarvestRadiusUpgrader : UpgraderBase
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
      if (character.Wallet.TryPayBill(_upgraderConfig.HarvestScaleCostsByLevel(_currentLevel + 1)))
      {
        _currentLevel++;
        character.Harvester.SetHarvestScale(character.Harvester.HarvestScale + _upgraderConfig.HarvestScaleDeltaByLevel);
        
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
      UpgraderUi.SetValues($"Harvest Radius\nLevel {_currentLevel}", NextLevelBill());
      UpgraderUi.AnimateTextUpdate();
    }
    
    private Bill NextLevelBill() => _upgraderConfig.HarvestScaleCostsByLevel(_currentLevel + 1);
  }
}