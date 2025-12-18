using DG.Tweening;
using Features.Units.Character.Inventory;
using Features.Units.Character.Inventory.Item;
using TMPro;
using UnityEngine;

namespace UI
{
  public class LootView : MonoBehaviour
  {
    [field: SerializeField] public ItemType ItemType { get; private set; }

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _pulseScale = 1.2f;
    [SerializeField] private float _pulseDuration = 0.3f;
    [SerializeField] private Color _pulseColor = Color.green;

    private Tween _currentTween;
    private Color _originalColor;

    private void Awake()
    {
      _originalColor = _text.color;
    }

    public void SetText(string text)
    {
      _text.text = text;
    }

    public void AnimateChange()
    {
      if (_currentTween != null && _currentTween.IsActive())
        _currentTween.Kill();

      var sequence = DOTween.Sequence();
      
      sequence.Append(_text.transform.DOScale(_pulseScale, _pulseDuration * 0.5f).SetEase(Ease.OutQuad));
      sequence.Join(_text.DOColor(_pulseColor, _pulseDuration * 0.5f));
      sequence.Append(_text.transform.DOScale(1f, _pulseDuration * 0.5f).SetEase(Ease.InQuad));
      sequence.Join(_text.DOColor(_originalColor, _pulseDuration * 0.5f));

      _currentTween = sequence;
    }
  }
}