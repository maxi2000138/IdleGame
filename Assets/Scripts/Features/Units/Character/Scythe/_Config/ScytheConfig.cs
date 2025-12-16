using System;
using UnityEngine;

namespace Features.Units.Character.Scythe._Config
{
  [Serializable]
  public class ScytheConfig
  {
    public Scythe Prefab;
    [Range(0f, 5f)] public float UseInterval;
    [Range(0f, 8f)] public float UseDistance;
    [Range(0f, 8f)] public float DetectionDistance;
  }
}