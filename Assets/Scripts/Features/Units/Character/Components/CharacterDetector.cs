using System.Collections.Generic;
using System.Linq;
using Features.Units.Character._Config;
using UnityEngine;
using Utils;

namespace Features.Units.Character
{
  public class CharacterDetector : MonoBehaviour
  {
    private CharacterConfig _config;

    private readonly Collider[] _overlapHits = new Collider[128];
    private Character _character;

    public void Construct(Character character, CharacterConfig characterConfig)
    {
      _character = character;
      _config = characterConfig;
    }
    
    public bool DetectGrass(out Farming.Grass grass)
    {
      var spherePosition = _character.transform.position + 
                           _character.transform.forward * _config.DetectionDistance;
      var sphereRadius = _config.DetectionDistance/2f;

      grass = SphereCast<Farming.Grass>(spherePosition, sphereRadius, Layer.Grass).FirstOrDefault();
      return grass != null;
    }
    
    private IEnumerable<T> SphereCast<T>(Vector3 position, float radius, int layerMask)
    {
      int hitCount = OverlapSphere(position, radius, _overlapHits, layerMask);
      
      DrawDebug(position, radius, 1f, Color.red);
      
      for (int i = 0; i < hitCount; i++)
      {
        if (_overlapHits[i].TryGetComponent<T>(out var component))
          yield return component;
      }
    }
    
    private int OverlapSphere(Vector3 worldPos, float radius, Collider[] hits, int layerMask) =>
      Physics.OverlapSphereNonAlloc(worldPos, radius, hits, layerMask);
    
    private static void DrawDebug(Vector3 worldPos, float radius, float seconds, Color color)
    {
      Debug.DrawRay(worldPos, radius * Vector3.up, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.down, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.left, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.right, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.forward, color, seconds);
      Debug.DrawRay(worldPos, radius * Vector3.back, color, seconds);
    }
  }
}