using UnityEngine;
using Es.InkPainter;

namespace Features.Farming
{
  public class Grass : MonoBehaviour
  {
    [field: SerializeField] public InkCanvas GrassCanvas { get; private set; }
  }
}