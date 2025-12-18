using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Features.Grass
{
  public class GrassRegenerator : MonoBehaviour
  {
    [SerializeField] private float _restorePercentage = 0.8f;
    [SerializeField] private float _checkInterval = 0.5f;
    [SerializeField] private float _growthDuration = 2f;

    private Grass _grass;
    private MeshRenderer _meshRenderer;
    private MaterialPropertyBlock _propertyBlock;
    private float _checkTimer;
    private bool _isGrowing;
    private static readonly int GrowthMultiplierId = Shader.PropertyToID("_GrowthMultiplier");

    private void Start()
    {
      _grass = GetComponent<Grass>();
      _meshRenderer = GetComponent<MeshRenderer>();
      _propertyBlock = new MaterialPropertyBlock();
      
      _meshRenderer.GetPropertyBlock(_propertyBlock);
      _propertyBlock.SetFloat(GrowthMultiplierId, 1f);
      _meshRenderer.SetPropertyBlock(_propertyBlock);
    }

    private void Update()
    {
      if (_isGrowing)
        return;

      _checkTimer += Time.deltaTime;
      if (_checkTimer < _checkInterval)
        return;

      _checkTimer = 0f;
      
      if (GetCutPercentage() >= _restorePercentage)
        RestoreGrassAsync().Forget();
    }

    private float GetCutPercentage()
    {
      var renderTexture = _grass.GrassCanvas.PaintDatas.First().paintMainTexture;
      var texture2D = ReadTextureFromRenderTexture(renderTexture);
      var pixels = texture2D.GetPixels();
      var cutPixels = pixels.Count(pixel => pixel.r > 0.9f && pixel.g > 0.9f && pixel.b > 0.9f);

      Object.Destroy(texture2D);
      return (float)cutPixels / pixels.Length;
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

    private async UniTaskVoid RestoreGrassAsync()
    {
      _isGrowing = true;
      _grass.GrassCanvas.ResetPaint();

      _meshRenderer.GetPropertyBlock(_propertyBlock);
      _propertyBlock.SetFloat(GrowthMultiplierId, 0f);
      _meshRenderer.SetPropertyBlock(_propertyBlock);

      var elapsedTime = 0f;
      while (elapsedTime < _growthDuration)
      {
        elapsedTime += Time.deltaTime;
        var growthMultiplier = Mathf.SmoothStep(0f, 1f, elapsedTime / _growthDuration);

        _meshRenderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetFloat(GrowthMultiplierId, growthMultiplier);
        _meshRenderer.SetPropertyBlock(_propertyBlock);

        await UniTask.Yield();
      }

      _meshRenderer.GetPropertyBlock(_propertyBlock);
      _propertyBlock.SetFloat(GrowthMultiplierId, 1f);
      _meshRenderer.SetPropertyBlock(_propertyBlock);

      _isGrowing = false;
    }
  }
}

