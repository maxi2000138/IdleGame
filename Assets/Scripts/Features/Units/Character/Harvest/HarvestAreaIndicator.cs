using UnityEngine;

namespace Features.Units.Character.Harvest
{
  [RequireComponent(typeof(LineRenderer))]
  public class HarvestAreaIndicator : MonoBehaviour
  {
    [SerializeField] private Harvester _harvester;
    [SerializeField] private float _radiusMultiplier = 1f;
    [SerializeField] private int _circleSegments = 64;
    [SerializeField] private float _heightOffset = 0.1f;

    private LineRenderer _lineRenderer;
    private Vector3[] _circlePoints;

    private void Awake()
    {
      _lineRenderer = GetComponent<LineRenderer>();
      _lineRenderer.useWorldSpace = true;
      _lineRenderer.loop = true;
      _lineRenderer.positionCount = _circleSegments;
      _lineRenderer.startWidth = 0.05f;
      _lineRenderer.endWidth = 0.05f;
      _lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
      _lineRenderer.receiveShadows = false;
      
      _circlePoints = new Vector3[_circleSegments];
    }

    private void Update()
    {
      if (_harvester == null)
        return;

      if (!_harvester.Enabled)
      {
        _lineRenderer.enabled = false;
        return;
      }

      _lineRenderer.enabled = true;
      UpdateCircle();
    }

    private void UpdateCircle()
    {

      var radius = _harvester.HarvestScale * _radiusMultiplier;
      var centerPosition = _harvester.transform.position;
      centerPosition.y += _heightOffset;

      for (var i = 0; i < _circleSegments; i++)
      {
        var angle = 2f * Mathf.PI * i / _circleSegments;
        _circlePoints[i] = centerPosition + new Vector3(
          Mathf.Cos(angle) * radius,
          0f,
          Mathf.Sin(angle) * radius
        );
      }

      _lineRenderer.SetPositions(_circlePoints);
    }
  }
}
