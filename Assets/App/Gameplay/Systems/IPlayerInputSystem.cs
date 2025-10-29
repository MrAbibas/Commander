using UnityEngine;

namespace App.Gameplay.Systems
{
    public interface IPlayerInputSystem
    {
        Vector2 MoveDirection { get; }
    }
}
