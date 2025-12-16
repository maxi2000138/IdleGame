using System;
using Features.Farm.Plant;
using UnityEngine;

namespace Features.Farm._Config
{
  [Serializable]
  public class FarmConfig
  {
    public Vector2Int FarmSize;
    public Vector3 FarmTileSize;
    public float CharacterSlowFactor;
    public float PlantGrowTime;
    public Plant.Plant PlantPrefab;
    public PlantLoot LootPrefab;
  }
}