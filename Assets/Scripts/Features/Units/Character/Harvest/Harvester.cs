using System;
using Es.InkPainter;
using Features.Farm;
using Features.Farming;
using Features.Units.Character.Scythe._Config;
using UnityEngine;

namespace Features.Units.Character.Scythe
{
  public class Harvester : MonoBehaviour
  {
    private HarvesterConfig _harvesterConfig;
    private Brush _cutBrush;
    private Grass _currentGrass;

    public bool Enabled { get; private set; }
    public float SlowFactor => _harvesterConfig.CharacterSlowFactor;

    public void Construct(HarvesterConfig harvesterConfig)
    {
      _harvesterConfig = harvesterConfig;
      _cutBrush = new Brush(harvesterConfig.HarvestTexture, _harvesterConfig.HarvestScale, Color.white);
    }
    
    public void Enable(Grass grass)
    {
      Enabled = true;
      gameObject.SetActive(true);

      _currentGrass = grass;
    }

    public void Disable()
    {
      Enabled = false;
      gameObject.SetActive(false);
    }
    
    public void Update()
    {
      if(!Enabled) 
        return;

      _currentGrass.GrassCanvas.Paint(
        _cutBrush,
        new Vector3(transform.position.x, _currentGrass.GrassCanvas.transform.position.y, transform.position.z)
      );
    }
  }
}