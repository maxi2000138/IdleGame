using System;
using UnityEngine;

namespace Features.Units.Character.Harvest._Config
{
  [Serializable]
  public class HarvesterConfig
  {
    [Range(0f, 1f)] public float CharacterSlowFactor;
    public Texture HarvestTexture;
    [Range(0f, 1f)] public float HarvestScale;
    [Range(0.01f, 0.5f)] public float SpawnItemCutPercentage;
  }
}