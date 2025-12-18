using System;
using UnityEngine;
using Es.InkPainter;
using Utils;

namespace Features.Farming
{
  public class Grass : MonoBehaviour
  {
    [field: SerializeField] public GrassItem GrassItem { get; private set; }
    [field: SerializeField] public InkCanvas GrassCanvas { get; private set; }
    
    public AnimationsConfig AnimationConfig;

    [Serializable]
    public class AnimationsConfig
    {
      public Color CutColor;
    }
  }
}