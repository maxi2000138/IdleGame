using Es.InkPainter;
using Features.Grass;
using Features.Units.Character.Harvest._Config;
using Infrastructure.Factory;
using UnityEngine;

namespace Features.Units.Character.Harvest
{
  public class Harvester : MonoBehaviour
  {
    [SerializeField] private ParticleSystem _cutEffect;

    private float _totalCutPixels;
    private Brush _cutBrush;
    private Grass.Grass _currentGrass;
    private IGameFactory _gameFactory;
    private HarvesterConfig _harvesterConfig;
    private GrassTextureHelper _grassTextureHelper;
    private Character _character;
    private const int MinTextureSize = 128;

    public bool Enabled { get; private set; }
    public float SlowFactor => _harvesterConfig.CharacterSlowFactor;
    public float HarvestScale => _cutBrush.Scale;

    public void Construct(Character character, HarvesterConfig harvesterConfig, IGameFactory gameFactory)
    {
      _character = character;
      _gameFactory = gameFactory;
      _harvesterConfig = harvesterConfig;
      _grassTextureHelper = new GrassTextureHelper();
      
      var scaledTexture = ScaleTextureIfNeeded(harvesterConfig.HarvestTexture);
      _cutBrush = new Brush(scaledTexture, harvesterConfig.HarvestScale, Color.white);
    }

    public void Enable(Grass.Grass grass)
    {
      Enabled = true;
      gameObject.SetActive(true);

      _currentGrass = grass;
      _grassTextureHelper.Initialize(grass.GrassCanvas);
    }

    public void Disable()
    {
      Enabled = false;
      gameObject.SetActive(false);
    } 

    public void Update()
    {
      if (!Enabled) return;

      _currentGrass.GrassCanvas.Paint(_cutBrush, BrushPosition());

      var (cutPixels, totalPixels) = _grassTextureHelper.ProcessCut();
      HandleCut(cutPixels, totalPixels);
    }

    public void SetHarvestScale(float scale)
    {
      _cutBrush.Scale = scale;
    }

    private void HandleCut(float cutPixels, float totalPixels)
    {
      if (cutPixels == 0)
        return;

      _totalCutPixels += cutPixels;

      if (ShouldPlayCutParticle(cutPixels))
        PlayCutParticle();

      if (ShouldSpawnItem(totalPixels))
      {
        _totalCutPixels = 0;
        SpawnAndAddItemToInventory();
      }
    }

    private bool ShouldSpawnItem(float totalPixels) =>
      _totalCutPixels / totalPixels > _harvesterConfig.SpawnItemCutPercentage;

    private void SpawnAndAddItemToInventory()
    {
      if (_character.Inventory.CanPushItem)
      {
        var item = _gameFactory.SpawnItem(_currentGrass.GrassItem, BrushPosition());
        _character.Inventory.TryPushItem(item);
      }
    }

    private void PlayCutParticle()
    {
      _cutEffect.startColor = _currentGrass.AnimationConfig.CutColor;
      _cutEffect.Play();
    }

    private bool ShouldPlayCutParticle(float cutPixels) => cutPixels >= 1;

    private Vector3 BrushPosition() =>
      new Vector3(transform.position.x, _currentGrass.GrassCanvas.transform.position.y, transform.position.z);

    private Texture ScaleTextureIfNeeded(Texture originalTexture)
    {
      if (originalTexture == null)
        return originalTexture;

      var width = originalTexture.width;
      var height = originalTexture.height;

      if (width >= MinTextureSize && height >= MinTextureSize)
        return originalTexture;

      var targetSize = Mathf.Max(MinTextureSize, Mathf.Max(width, height));
      
      var renderTexture = RenderTexture.GetTemporary(targetSize, targetSize, 0, RenderTextureFormat.Default);
      Graphics.Blit(originalTexture, renderTexture);
      
      var scaledTexture = new Texture2D(targetSize, targetSize, TextureFormat.RGBA32, false);
      RenderTexture.active = renderTexture;
      scaledTexture.ReadPixels(new Rect(0, 0, targetSize, targetSize), 0, 0);
      scaledTexture.Apply();
      
      RenderTexture.active = null;
      RenderTexture.ReleaseTemporary(renderTexture);

      return scaledTexture;
    }
  }
}