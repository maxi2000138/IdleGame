using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Features.Units.Character.Currency;
using TMPro;
using UI;
using UnityEngine;

namespace Features.Upgraders
{
  public class UpgraderUi : MonoBehaviour
  {
    public TextMeshProUGUI MainText;
    public List<LootView> LootViews;

    [SerializeField] private AnimationConfig _animationConfig;
    
    private Tween _currentTween;
    private Color _originalTextColor;
    private Vector3 _originalScale;

    private void Awake()
    {
      _originalTextColor = MainText.color;
      _originalScale = transform.localScale;
    }

    public void SetValues(string mainText, Bill bill)
    {
      MainText.text = mainText;
      
      for (int i = 0; i < LootViews.Count; i++)
      {
        var item = bill.Items.First(x => x.Item == LootViews[i].ItemType);
        LootViews[i].SetText(item.Amount.ToString());
      }
    }
    
    public void AnimatePunch()
    {
      if (_currentTween != null && _currentTween.IsActive())
        _currentTween.Kill();

      _currentTween = transform.DOPunchScale(_animationConfig.PunchScale, _animationConfig.PunchDuration);
    }

    public void AnimateSuccess()
    {
      if (_currentTween != null && _currentTween.IsActive())
        _currentTween.Kill();

      var sequence = DOTween.Sequence();
      
      sequence.Append(transform.DOScale(_animationConfig.SuccessScale, _animationConfig.SuccessDuration * 0.3f).SetEase(Ease.OutBack));
      sequence.Append(transform.DOScale(_originalScale, _animationConfig.SuccessDuration * 0.7f).SetEase(Ease.InBack));
      sequence.Join(MainText.DOColor(_animationConfig.SuccessColor, _animationConfig.SuccessDuration * 0.3f));
      sequence.Append(MainText.DOColor(_originalTextColor, _animationConfig.SuccessDuration * 0.7f));

      foreach (var lootView in LootViews)
        lootView.AnimateChange();

      _currentTween = sequence;
    }

    public void AnimateNotEnoughMoney()
    {
      if (_currentTween != null && _currentTween.IsActive())
        _currentTween.Kill();

      var sequence = DOTween.Sequence();
      
      sequence.Append(transform.DOShakePosition(_animationConfig.ShakeDuration, _animationConfig.ShakeStrength));
      sequence.Join(MainText.DOColor(_animationConfig.ErrorColor, _animationConfig.ShakeDuration * 0.5f));
      sequence.Append(MainText.DOColor(_originalTextColor, _animationConfig.ShakeDuration * 0.5f));

      _currentTween = sequence;
    }

    public void AnimateTextUpdate()
    {
      MainText.transform.DOPunchScale(_animationConfig.TextPunchScale, _animationConfig.TextPunchDuration);
    }

    public void AnimateEnter()
    {
      transform.DOScale(_animationConfig.EnterScale, _animationConfig.EnterDuration).SetEase(Ease.OutBack);
    }

    public void AnimateExit()
    {
      transform.DOScale(_originalScale, _animationConfig.ExitDuration).SetEase(Ease.InBack);
    }

    private void OnDestroy()
    {
      _currentTween?.Kill();
    }

    [Serializable]
    public class AnimationConfig
    {
      public Vector3 PunchScale = new Vector3(0.1f, 0.1f, 0.1f);
      public float PunchDuration = 0.2f;
      
      public Vector3 SuccessScale = new Vector3(1.15f, 1.15f, 1.15f);
      public float SuccessDuration = 0.6f;
      public Color SuccessColor = Color.green;
      
      public float ShakeDuration = 0.4f;
      public float ShakeStrength = 10f;
      public Color ErrorColor = Color.red;
      
      public Vector3 TextPunchScale = new Vector3(0.05f, 0.05f, 0.05f);
      public float TextPunchDuration = 0.15f;
      
      public Vector3 EnterScale = new Vector3(1.05f, 1.05f, 1.05f);
      public float EnterDuration = 0.3f;
      
      public float ExitDuration = 0.2f;
    }
  }
}