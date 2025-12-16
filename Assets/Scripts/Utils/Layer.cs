using UnityEngine;

namespace Utils
{
  public static class Layer
  {
    public static int Grass = 1 << LayerMask.NameToLayer(nameof(Grass));
  }
}