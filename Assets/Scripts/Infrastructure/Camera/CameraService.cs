using Unity.Cinemachine;
using UnityEngine;

namespace Infrastructure.Camera
{
  public class CameraService : MonoBehaviour, ICameraService
  {
    [SerializeField] private UnityEngine.Camera _camera;
    [SerializeField] private CinemachineCamera _cameraZoom;

    public UnityEngine.Camera Camera => _camera;

    public void SetTarget(Transform target)
    {
      _cameraZoom.Follow = target;
      _cameraZoom.LookAt = target;
    }
  }
}