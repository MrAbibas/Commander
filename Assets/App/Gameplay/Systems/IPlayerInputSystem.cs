using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace App.Gameplay.Systems
{
    public interface IPlayerInputSystem
    {
        Vector2 MoveDirection { get; }
    }

    public class PlayerInputSystem : IPlayerInputSystem, ITickable
    {
        public Vector2 MoveDirection { get; private set; }

        public void Tick()
        {
            MoveDirection = Joystick.current != null ? Joystick.current.stick.ReadValue() : Vector2.zero;
        }
    }
}
