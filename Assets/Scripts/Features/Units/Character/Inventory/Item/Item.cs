using UnityEngine;

namespace Features.Units.Character.Inventory.Item
{
  public class Item : MonoBehaviour
  {
    [field: SerializeField] public ItemType Type { get; private set; }
  }
}