using System;
using Es.InkPainter;
using UnityEngine;

namespace Features.Grass
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