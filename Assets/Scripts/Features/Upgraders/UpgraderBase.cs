using System.Threading;
using Cysharp.Threading.Tasks;
using Features.Units.Character;
using Features.Upgraders;
using UnityEngine;

public abstract class UpgraderBase : MonoBehaviour
{
  [SerializeField] protected UpgraderUi UpgraderUi;
  
  private int _upgradeDelayMS;
  private CancellationTokenSource _cancellationSource;

  public abstract void TryUpgrade(Character character);
  
  public void Construct(int upgradeDelayMS)
  {
    _upgradeDelayMS = upgradeDelayMS;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.TryGetComponent<Character>(out var character))
    {
      _cancellationSource = new CancellationTokenSource();
      ProcessUpgrade(character, _cancellationSource.Token).Forget();
    }
  }
  
  private void OnTriggerExit(Collider other)
  {
    _cancellationSource.Cancel();
    _cancellationSource = null;
  }
  
  private async UniTask ProcessUpgrade(Character character, CancellationToken cancellationToken)
  {
    while (cancellationToken.IsCancellationRequested == false)
    { 
      await UniTask.Delay(_upgradeDelayMS);
      
      if(cancellationToken.IsCancellationRequested) break;
      TryUpgrade(character);
    }
  }
}
