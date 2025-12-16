using UnityEngine;

namespace Input
{
    public interface IJoystickService
    {
        Vector2 GetAxis();
        float GetDeadZone();
        void Init();
        void Enable(bool isEnable);
    }
}