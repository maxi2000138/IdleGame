using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

[RequireComponent(typeof(Renderer))]
public class TextureScroll : MonoBehaviour
{
  [SerializeField]  Vector2 _scrollSpeed = new Vector2(0.5f, 0.5f);
  [SerializeField] private int _updateIntervalMs = 16;

  private Material _material;
  private CancellationTokenSource _cts;

  private void Awake()
  {
    _material = GetComponent<Renderer>().material;
    _cts = new CancellationTokenSource();
  }

  private void Start()
  {
    StartScrolling().Forget();
  }

  private async UniTaskVoid StartScrolling()
  {
    Vector2 offset = Vector2.zero;

    while (!_cts.IsCancellationRequested)
    {
      offset += _scrollSpeed * (_updateIntervalMs / 1000f);
      _material.mainTextureOffset = offset;

      await UniTask.Delay(
        _updateIntervalMs,
        DelayType.DeltaTime,
        PlayerLoopTiming.Update,
        _cts.Token
      );
    }
  }

  private void OnDestroy()
  {
    _cts?.Cancel();
    _cts?.Dispose();
  }

  private void OnDisable()
  {
    _cts?.Cancel();
  }
}