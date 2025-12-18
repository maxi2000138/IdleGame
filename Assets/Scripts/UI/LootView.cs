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

    public void SetText(string text)
    {
      _text.text = text;
    }
  }
}