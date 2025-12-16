using System;
using Features.Farm.Plant;
using UnityEngine;

namespace Features.Farm._Config
{
  [Serializable]
  public class FarmConfig
  {
    public Vector3 FarmTileSize;
    public float CharacterSlowFactor;
    public Vector2Int LootAmountRange;
    public float PlantGrowTime;
    public Soil.Soil SoilPrefab;
    public Plant.Plant PlantPrefab;
    public PlantLoot LootPrefab;
  }
}