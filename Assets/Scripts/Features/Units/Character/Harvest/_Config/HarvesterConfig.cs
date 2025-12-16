using System;
using UnityEngine;

namespace Features.Units.Character.Scythe._Config
{
  [Serializable]
  public class HarvesterConfig
  {
    [Range(0f,1f)] public float CharacterSlowFactor;
    public Texture HarvestTexture;
    [Range(0f,1f)] public float HarvestScale;
  }
}