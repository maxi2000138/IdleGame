using Features.Farm._Config;
using Services;
using UnityEngine;

namespace Features.Farm
{
  public class Farm : MonoBehaviour
  {
    private IGameFactory _gameFactory;
    private FarmConfig _config;

    public void Construct(IGameFactory gameFactory, FarmConfig farmConfig)
    {
      _gameFactory = gameFactory;
      _config = farmConfig;
    }
    
    public void CreateFarmField()
    {
      for (int i = 0; i < _config.FarmSize.x; i++)
      {
        for (int j = 0; j < _config.FarmSize.y; j++)
        {
          var position = transform.position + new Vector3(
            (i + 0.5f) * _config.FarmTileSize.x, 
            0, 
            (j + 0.5f) * _config.FarmTileSize.x);

          Plant.Plant plant = _gameFactory.CreatePlant(position, _config.FarmTileSize, transform);
          
          // plant.SetHarvestStatus(true); 
          // plant.SetGrowTime(_staticData.FarmConfig().PlantGrowTime);
          // soil.SetPlant(plant);
        }
      } 
    }
  }
}