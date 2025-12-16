using UnityEngine;

namespace Infrastructure.Camera
{
  public interface ICameraService
  {
    public UnityEngine.Camera Camera { get; }
    void SetTarget(Transform target);
  }
}