using UnityEngine;

namespace Utils
{
  public static class TextureHelper
  {
    public static float WhitePixelsPercentage(Texture2D texture)
    {
      Color[] pixels = texture.GetPixels();

      var whitePixels = 0;
      var totalPixels = pixels.Length;

      for (var i = 0; i < pixels.Length; i++)
      {
        if (IsWhitePixel(pixels[i]))
          whitePixels++;
      }

      return whitePixels / (float)totalPixels * 100f;

      bool IsWhitePixel(Color color) => color is { r: > 0.9f, g: > 0.9f, b: > 0.9f };
    }
  }
}