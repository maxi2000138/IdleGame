using System;
using UnityEngine;

namespace Features.Farm._Config
{
  [Serializable]
  public class GrassConfig
  {
    public Texture2D GrassBrushTexture;
    [Range(0f, 1f)]
    public float GrassBrushScale = 0.1f;
    public Color GrassBrushColor = Color.white;
  }
}