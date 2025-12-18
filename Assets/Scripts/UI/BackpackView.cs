using TMPro;
using UnityEngine;

namespace UI
{
  public class BackpackView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _text;

    public void SetValues(int currentSize, int maxSize)
    {
      _text.text = $"{currentSize}/{maxSize}";
    }
  }
}