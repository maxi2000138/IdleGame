using System.Linq;
using Es.InkPainter;
using UnityEngine;

namespace Features.Grass
{
  public class GrassTextureHelper
  {
    private Texture2D _lastTexture;
    private InkCanvas _inkCanvas;

    public void Initialize(InkCanvas inkCanvas)
    {
      _inkCanvas = inkCanvas;

      var renderTexture = GetRenderTexture();
      _lastTexture = ReadTextureFromRenderTexture(renderTexture);
    }

    public (float, float) ProcessCut()
    {
      var renderTexture = GetRenderTexture();
      var currentTexture = ReadTextureFromRenderTexture(renderTexture);
      var changePixels = 0;
      var totalPixels = currentTexture.width * currentTexture.height;

      Color[] currentPixels = currentTexture.GetPixels();
      Color[] lastPixels = _lastTexture.GetPixels();

      for (var i = 0; i < currentPixels.Length; i++)
      {
        if (IsWhitePixel(currentPixels[i]) && !IsWhitePixel(lastPixels[i]))
          changePixels++;
      }

      _lastTexture.SetPixels(currentPixels);
      _lastTexture.Apply();

      return (changePixels, totalPixels);
    }

    private Texture2D ReadTextureFromRenderTexture(RenderTexture rt)
    {
      var texture2D = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
      RenderTexture.active = rt;
      texture2D.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
      texture2D.Apply();
      RenderTexture.active = null;

      return texture2D;
    }

    private RenderTexture GetRenderTexture()
    {
      var paintSet = _inkCanvas.PaintDatas?.FirstOrDefault();
      return paintSet?.paintMainTexture;
    }

    private bool IsWhitePixel(Color color) => color is { r: > 0.9f, g: > 0.9f, b: > 0.9f };
  }
}