using Es.InkPainter;
using UnityEngine;

namespace Features.Farm
{
  public class Farm : MonoBehaviour
  {
    [field: SerializeField] public InkCanvas GrassCanvas { get; private set; }
  }
}