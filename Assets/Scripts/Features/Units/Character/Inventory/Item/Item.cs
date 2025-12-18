using UnityEngine;

namespace Features.Units.Character.Inventory
{
  public class Item : MonoBehaviour
  {
    [field: SerializeField] public ItemType Type { get; private set; }
  }
}